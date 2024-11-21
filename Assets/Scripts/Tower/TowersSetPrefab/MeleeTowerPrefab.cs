using UnityEngine;

public class MeleeTowerPrefab 
{
     private GameObject _meleeTowerPrefab;
    
    public MeleeTowerPrefab(GameObject meleeTowerPrefab) 
        => _meleeTowerPrefab = meleeTowerPrefab;

    public GameObject GetMeleeTowerPrefab => _meleeTowerPrefab;
}