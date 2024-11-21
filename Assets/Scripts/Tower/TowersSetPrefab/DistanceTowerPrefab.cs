using UnityEngine;

public class DistanceTowerPrefab
{
    private GameObject _distanceTowerPrefab;

    public DistanceTowerPrefab(GameObject distanceTowerPrefab) 
        => _distanceTowerPrefab = distanceTowerPrefab;

    public GameObject GetDistanceTowerPrefab => _distanceTowerPrefab;
}