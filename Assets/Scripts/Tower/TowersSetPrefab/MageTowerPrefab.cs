using UnityEngine;

public class MageTowerPrefab 
{
    private GameObject _mageTowerPrefab;

    public MageTowerPrefab(GameObject mageTowerPrefab) 
        => _mageTowerPrefab = mageTowerPrefab;

    public GameObject GetMageTowerPrefab => _mageTowerPrefab;
}