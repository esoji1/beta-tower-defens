using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Config", fileName = "Config")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public RuningStateConfig RuningStateConfig { get; private set; }
}
