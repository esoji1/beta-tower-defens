using Assets.Scripts.Units.Enemies;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Units.Enemies;
using UnityEngine;

public class Werewolf : MonoBehaviour, IDamage, Assets.Scripts.EmenyWaveSpawner.IMovable
{
    [SerializeField] private WerewolfConfig _config;
    [SerializeField] private WerewolfView _werewolfView;
    
    private Health _health;
    private List<Transform> _targets;
    private Transform _currentTarget;
    private bool _isAttacking = false;
    private int _betweenAttacks = 1;
    private int _armor = 50;
    private bool _isMoving = true;

    private SearchForEnemies _searchForEnemies;
    private ChangeEnemyPosition _changeEnemyPosition;
    private PointByPoint _pointByPoint;

    public Health Health => _health;
    public Transform Transform => transform;
    public float Speed => _config.Speed;

    private void Awake()
    {
        _health = new Health(100);
        _targets = new List<Transform>();
        _searchForEnemies = new SearchForEnemies();
        _werewolfView.Initialize();
        _changeEnemyPosition = new ChangeEnemyPosition();
    }

    private void Start()
    {
        StartCoroutine(_changeEnemyPosition.SetRandomPosition(_config.AttackRadius));
    }

    private void Update()
    {
        Vector3 direction = (_pointByPoint.CurrentTarget - transform.position).normalized;
        UnityRtation(direction.x);

        if (_pointByPoint != null)
        {
            _pointByPoint.Update();
            _pointByPoint.StartMove();
        }

        AdvancementInita();
        
        if (_currentTarget == null)
        {
            _searchForEnemies.FindNewTargets(_targets, transform, _config);
            SetNextTarget();
        }
        else
        {
            if (Vector2.Distance(transform.position, _currentTarget.position) < _config.VisibilityRadius)
            {
                MoveToTarget();
                TakeDamage(_config.DamageAmount);
            }
            else
            {
                _currentTarget = null;
                StopAttacking();
            }
        }

        ApplyOffset();
    }

    public void Initialize(Transform[] targets)
    {
        _pointByPoint = new PointByPoint(this, targets.Select(point => point.position));
    }

    public void TakeDamage(int damage)
    {
        if (_currentTarget.TryGetComponent(out IDamage units))
        {
            if (Vector2.Distance(transform.position, _currentTarget.position) <= _config.AttackRadius && !_isAttacking)
            {
                StartCoroutine(DelayBetweenAttacks(_betweenAttacks, units, damage));

                _isAttacking = true;
                _isMoving = false;
            }

            if (units.Health.HealthValue <= 0)
            {
                Destroy(_currentTarget.gameObject);
                _targets.Remove(_currentTarget);
                SetNextTarget();
            }
        }
    }

    public void Damage(int damage)
    {
        _armor -= damage;

        if (_armor <= 0)
        {
            _health.TakeDamage(damage);
            _armor = 0;
        }

        if (_health.HealthValue <= 0)
        {
        }
    }

    private void MoveToTarget()
    {
        if (_currentTarget == null)
            return;

        float distanceToTarget = Vector2.Distance(transform.position, _currentTarget.position);

        if (distanceToTarget > _config.AttackRadius)
        {
            _isMoving = true;
            
            Vector3 direction = (_currentTarget.position - transform.position).normalized;

            UnityRtation(direction.x);
            
            float distanceThisFrame = _config.Speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(
                transform.position, _currentTarget.position + _changeEnemyPosition.AddRandomPositionToGo, distanceThisFrame);
        }
    }

    private void SetNextTarget()
    {
        if (_targets.Count > 0)
        {
            _currentTarget = _targets[0];
            _isAttacking = false;
        }
        else
        {
            _currentTarget = null;
        }
    }

    private IEnumerator DelayBetweenAttacks(int between, IDamage units, int damage)
    {
        if (units != null)
        {
            while (units.Health.HealthValue > 0 && _currentTarget != null &&
                   Vector2.Distance(transform.position, _currentTarget.position) <= _config.AttackRadius)
            {
                yield return new WaitForSeconds(between);
                units.Damage(damage);
            }
        }

        StopAttacking();
    }

    private void StopAttacking() => _isAttacking = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _config.VisibilityRadius);
    }
    
    private void AdvancementInita()
    {
        if (_isMoving)
        {
            _werewolfView.StartRuning();
            _werewolfView.StopAttacking();
        }
        else
        {
            _werewolfView.StartAttacking();
            _werewolfView.StopRuning();
        }
    }

    private bool UnityRtation(float direction)
        => _werewolfView.SpriteRenderer.flipX = direction < 0;

    private void ApplyOffset()
    {
        Vector3 offset = _changeEnemyPosition.AddRandomPositionToGo;
        transform.position += offset * Time.deltaTime;
    }
}