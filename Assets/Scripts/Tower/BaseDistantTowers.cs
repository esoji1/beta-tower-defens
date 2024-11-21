using UnityEngine;

public abstract class BaseDistantTowers : MonoBehaviour, IBaseTower
{
    protected LayerMask _layerMask;
    protected FirePoint _firePoint;
    protected float _fireCountdown = 0f;
    protected Transform _currentTarget;

    protected abstract float TimeBetweenShots { get; }
    protected abstract float AttackRadius { get; }
    protected abstract GameObject ProjectilePrefab { get; }

    private void Update()
    {
        _fireCountdown -= Time.deltaTime;

        if (_currentTarget == null || IsTargetInRange(_currentTarget) == false)
            FindNewTarget();

        if (_currentTarget != null && _fireCountdown <= 0f)
        {
            Shoot(_currentTarget);
            _fireCountdown = TimeBetweenShots;
        }
    }

    public void Initialize(LayerMask layerMask)
    {
        _layerMask = layerMask;
        ConfigureFirePoint();
    }

    public void FindNewTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, AttackRadius, _layerMask);

        if (hitColliders.Length == 0)
        {
            _currentTarget = null;
            return;
        }

        _currentTarget = hitColliders[0].transform;
    }

    protected abstract void ConfigureFirePoint();

    protected void Shoot(Transform target)
    {
        GameObject projectileObject = Instantiate(ProjectilePrefab, _firePoint.transform.position, Quaternion.identity);

        Vector3 direction = (target.position - _firePoint.transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectileObject.transform.rotation = ForwardArrowRotation(angle);

        if (projectileObject.TryGetComponent(out IProjectile projectile))
            projectile.SetTarget(target);
    }

    protected Quaternion ForwardArrowRotation(float angle) => Quaternion.Euler(new Vector3(0, 0, angle));

    private bool IsTargetInRange(Transform target) 
        => Vector3.Distance(transform.position, target.position) <= AttackRadius;
}