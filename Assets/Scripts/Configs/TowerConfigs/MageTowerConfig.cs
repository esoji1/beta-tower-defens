using UnityEngine;

[CreateAssetMenu(menuName = "TowerConfigs/MageTowerConfig", fileName = "MageConfig")]
public class MageTowerConfig : BaseTowerConfig
{
    [SerializeField] private float _radius;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private GameObject _magePrefab;
    
    public override float Radius => _radius;
    public override float TimeBetweenShots => _timeBetweenShots;
    public override GameObject ProjectilePrefab => _magePrefab;
}