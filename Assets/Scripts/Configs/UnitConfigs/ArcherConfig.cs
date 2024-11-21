using UnityEngine;

[CreateAssetMenu(menuName = "EnemyConfigs/ArcherConfig", fileName = "ArcherConfig")]
public class ArcherConfig : EnemyConfig
{
    [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
    
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _attackRadius = 3f;
    [SerializeField] private float _visibilityRadius = 4;
    [SerializeField] private int _damageAmount = 30;

    public override LayerMask LayerMask => _layerMask;
    public override float Speed => _speed;
    public override float AttackRadius => _attackRadius;
    public override float VisibilityRadius => _visibilityRadius;
    public override int DamageAmount => _damageAmount;
}