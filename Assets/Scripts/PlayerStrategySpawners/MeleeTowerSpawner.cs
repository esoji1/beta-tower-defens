using UnityEngine;

public class MeleeTowerSpawner : SpawnerTower
{
    private GameObject _prefabMeleeTower;
    private IMovable _movable;

    public MeleeTowerSpawner(IMovable movable, GameObject prefabTower, SpawnArea clickedArea) 
        : base(movable, prefabTower, clickedArea)
    {
    }

    public override GameObject SpawnTowerPosition(Vector3 input)
    {
        GameObject spawnedTower = base.SpawnTowerPosition(input);
        Debug.Log("MeleeTowerSpawner");
        return spawnedTower;
    }
}