using UnityEngine;

public abstract class EnemyConfig : ScriptableObject
{
    public abstract LayerMask LayerMask { get; }
    public abstract float Speed { get; }
    public abstract float AttackRadius{ get; }
    public abstract float VisibilityRadius { get; }
    public abstract int DamageAmount { get; }
}