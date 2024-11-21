using UnityEngine;

[CreateAssetMenu(menuName = "EnemyConfigs/OrcConfig", fileName = "OrcConfig")]
public class OrcConfig : EnemyConfig
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private float _attackRadius = 0.5f;
    [SerializeField] private float _visibilityRadius = 1.5f;
    [SerializeField] private int _damageAmount = 20;

    public override LayerMask LayerMask => _layerMask;
    public override float Speed => _speed;
    public override float AttackRadius => _attackRadius;
    public override float VisibilityRadius => _visibilityRadius;
    public override int DamageAmount => _damageAmount;
}