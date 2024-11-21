using Assets.Scripts.Units.Enemies;
using System.Collections.Generic;
using System.Linq;
using Units.Enemies;
using UnityEngine;

public class Archer : MonoBehaviour, IDamage, Assets.Scripts.EmenyWaveSpawner.IMovable
{
    [SerializeField] private ArcherConfig _config;
    [SerializeField] private ArcherView _archerView;

    private Health _health;
    private List<Transform> _targets;
    private Transform _currentTarget;
    private float _fireCountdown;
    private float _timeBetweenShots = 1f;
    private bool _isMoving = true;

    private SpawnProjectile _spawnProjectile;
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
        _archerView.Initialize();

        _spawnProjectile = GetComponentInChildren<SpawnProjectile>();
        _changeEnemyPosition = new ChangeEnemyPosition();
    }

    private void Start()
    {
        StartCoroutine(_changeEnemyPosition.SetRandomPosition(2));
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

        _fireCountdown -= Time.deltaTime;

        if (_currentTarget == null)
        {
            _searchForEnemies.FindNewTargets(_targets, transform, _config);
            SetNextTarget();
        }
        else
        {
            if (Vector2.Distance(transform.position, _currentTarget.position) <= _config.VisibilityRadius)
            {
                MoveToTarget();
                TakeDamage();
            }
            else
            {
                _currentTarget = null;
            }
        }

        ApplyOffset();
    }

    public void Initialize(Transform[] targets)
    {
        _pointByPoint = new PointByPoint(this, targets.Select(point => point.position));
    }

    private void TakeDamage()
    {
        if (_currentTarget != null &&
            Vector2.Distance(transform.position, _currentTarget.position) <= _config.AttackRadius)
        {
            if (_fireCountdown <= 0f)
            {
                if (_currentTarget.TryGetComponent(out IDamage targetDamage) && targetDamage.Health.HealthValue > 0)
                {
                    _isMoving = false;

                    Shoot();
                    _fireCountdown = _timeBetweenShots;
                }
                else
                {
                    Destroy(_currentTarget.gameObject);
                    _currentTarget = null;
                    _targets.Remove(_currentTarget);
                    SetNextTarget();
                }
            }
        }
    }

    private void Shoot()
    {
        if (_currentTarget != null)
        {
            GameObject projectileObject =
                Instantiate(_config.ProjectilePrefab, _spawnProjectile.SpawnArrow.position, Quaternion.identity);

            Vector3 direction = (_currentTarget.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectileObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (projectileObject.TryGetComponent(out IProjectile projectile))
                projectile.SetTarget(_currentTarget);
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
        }
    }

    public void Damage(int damage)
    {
        _health.TakeDamage(damage);

        if (_health.HealthValue <= 0)
        {
        }
    }

    private void AdvancementInita()
    {
        if (_isMoving)
        {
            _archerView.StartRuning();
            _archerView.StopAttacking();
        }
        else
        {
            _archerView.StartAttacking();
            _archerView.StopRuning();
        }
    }

    private void UnityRtation(float direction)
    {
        if (direction > 0)
        {
            _archerView.SpriteRenderer.flipX = false;

            SetParticlesPosition(0.6f);
        }
        else
        {
            _archerView.SpriteRenderer.flipX = true;

            SetParticlesPosition(-0.6f);
        }
    }

    public void SetParticlesPosition(float offset)
        => _spawnProjectile.SpawnArrow.localPosition = new Vector3(offset, 0, 0);

    private void ApplyOffset()
    {
        Vector3 offset = _changeEnemyPosition.AddRandomPositionToGo;
        transform.position += offset * Time.deltaTime;
    }
}