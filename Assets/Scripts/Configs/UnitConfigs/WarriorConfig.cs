using UnityEngine;

[CreateAssetMenu(menuName = "WarriorConfig", fileName = "WarriorConfig")]
public class WarriorConfig : ScriptableObject
{
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public float Speed { get; private set; } = 3.5f;
    [field: SerializeField] public float AttackRadius { get; private set; } = 0.5f;
    [field: SerializeField] public float VisibilityRadius { get; private set; } = 1.5f;
    [field: SerializeField] public int DamageAmount { get; private set; } = 10;
}