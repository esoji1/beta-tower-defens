using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "EnemySpawner/Wave")]
public class Wave : ScriptableObject
{
    [field: SerializeField] public List<EnemyGroup> EnemyGroups { get; private set; }
}