using UnityEngine;

[CreateAssetMenu(menuName = "RadiusPlayerSpawnPoint", fileName = "RadiusConfig")]
public class RadiusSpawnPoint : ScriptableObject
{
    [field: SerializeField, Range(1, max: 50)] public int Radius { get; private set; }
}