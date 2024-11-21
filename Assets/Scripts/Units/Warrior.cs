using Assets.Scripts.Units.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using Units.Enemies;
using UnityEngine;

public class Warrior : MonoBehaviour, IDamage
{
    [SerializeField] private WarriorConfig _config;

    private Transform _target;
    private int _speed = 3;
    private Health _health;

    private List<Transform> _targets;
    private Transform _currentTarget;
    private bool _isAttacking = false;
    private int _betweenAttacks = 1;

    private SearchForEnemies _searchForEnemies;
    private ChangeEnemyPosition _changeEnemyPosition;

    public Health Health => _health;
    public bool IsAlive => _health.HealthValue <= 0;

    public event Action OnDeath;

    private void Awake()
    {
        _health = new Health(100);
        _targets = new List<Transform>();
        _searchForEnemies = new SearchForEnemies();
        _changeEnemyPosition = new ChangeEnemyPosition();
    }

    private void Start()
    {
        StartCoroutine(_changeEnemyPosition.SetRandomPosition(_config.AttackRadius));
    }

    private void Update()
    {
        if (_currentTarget == null)
        {
            FindNewTargets();
            SetNextTarget();
        }
        else
        {
            if (Vector2.Distance(transform.position, _currentTarget.position) < _config.VisibilityRadius)
            {
                MoveToTargetWarrior();
                TakeDamage(_config.DamageAmount);
            }
            else
            {
                _currentTarget = null;
                StopAttacking();
            }
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        StartCoroutine(MoveToTarget());
    }

    public void TakeDamage(int damage)
    {
        if (_currentTarget.TryGetComponent(out IDamage units))
        {
            if (Vector2.Distance(transform.position, _currentTarget.position) <= _config.AttackRadius && !_isAttacking)
            {
                StartCoroutine(DelayBetweenAttacks(_betweenAttacks, units, damage));
                _isAttacking = true;
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
        _health.TakeDamage(damage);

        if (_health.HealthValue <= 0)
        {
        }
    }

    private void MoveToTargetWarrior()
    {
        if (_currentTarget == null)
            return;

        float distanceToTarget = Vector2.Distance(transform.position, _currentTarget.position);

        if (distanceToTarget > _config.AttackRadius)
        {
            Vector3 direction = (_currentTarget.position - transform.position).normalized;
            float distanceThisFrame = _config.Speed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(
                transform.position, _currentTarget.position + _changeEnemyPosition.AddRandomPositionToGo, distanceThisFrame);
        }
    }

    private void FindNewTargets()
    {
        _targets.Clear();
        Collider2D[] hitColliders =
            Physics2D.OverlapCircleAll(transform.position, _config.VisibilityRadius, _config.LayerMask);

        foreach (var hitCollider in hitColliders)
        {
            _targets.Add(hitCollider.transform);
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
            while (units.Health.HealthValue > 0 &&
                   Vector2.Distance(transform.position, _currentTarget.position) <= _config.AttackRadius)
            {
                yield return new WaitForSeconds(between);
                units.Damage(damage);
            }
        }

        StopAttacking();
    }

    private IEnumerator MoveToTarget()
    {
        while (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * _speed);

            if (Vector3.Distance(transform.position, _target.position) < 0.1f)
                yield break;

            yield return null;
        }
    }

    private void StopAttacking()
    {
        _isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _config.VisibilityRadius);
    }
}