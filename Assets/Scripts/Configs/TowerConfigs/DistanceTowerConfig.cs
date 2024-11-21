using UnityEngine;

[CreateAssetMenu(menuName = "TowerConfigs/DistanceTowerConfig", fileName = "DistanceConfig")]
public class DistanceTowerConfig : BaseTowerConfig
{
    [SerializeField] private float _radius;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private GameObject _arrowPrefab;
    
    public override float Radius => _radius;
    public override float TimeBetweenShots => _timeBetweenShots;
    public override GameObject ProjectilePrefab => _arrowPrefab;
}