using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGroup", menuName = "EnemySpawner/EnemyGroup")]
public class EnemyGroup : ScriptableObject
{
    [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
    [field: SerializeField] public float SpawnInterval { get; private set; }
}