using UnityEngine;

public class MageTower : BaseDistantTowers
{
    [SerializeField] private BaseTowerConfig _mageTowerConfig;

    protected override float TimeBetweenShots => _mageTowerConfig.TimeBetweenShots;
    protected override float AttackRadius => _mageTowerConfig.Radius;
    protected override GameObject ProjectilePrefab => _mageTowerConfig.ProjectilePrefab;

    protected override void ConfigureFirePoint() 
        => _firePoint = GetComponentInChildren<FirePoint>();
}