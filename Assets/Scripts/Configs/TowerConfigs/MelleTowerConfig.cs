using UnityEngine;

[CreateAssetMenu(menuName = "TowerConfigs/MelleTowerConfig", fileName = "MelleConfig")]
public class MelleTowerConfig : ScriptableObject
{
    [field: SerializeField] public float Radius { get; private set; }
}