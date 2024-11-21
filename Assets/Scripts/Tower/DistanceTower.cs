using UnityEngine;

public class DistanceTower : BaseDistantTowers
{
    [SerializeField] private BaseTowerConfig _distanceTowerConfig;

    protected override float TimeBetweenShots => _distanceTowerConfig.TimeBetweenShots;
    protected override float AttackRadius => _distanceTowerConfig.Radius;
    protected override GameObject ProjectilePrefab => _distanceTowerConfig.ProjectilePrefab;

    protected override void ConfigureFirePoint() =>
        _firePoint = GetComponentInChildren<FirePoint>();
}