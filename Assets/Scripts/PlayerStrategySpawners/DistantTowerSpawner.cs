using UnityEngine;

public class DistantTowerSpawner : SpawnerTower
{
    private GameObject _prefabDistantTower;
    private IMovable _movable;

    public DistantTowerSpawner(IMovable movable, GameObject prefabTower, SpawnArea clickedArea) 
        : base(movable, prefabTower, clickedArea)
    {
    }

    public override GameObject SpawnTowerPosition(Vector3 input)
    {
        GameObject spawnedTower = base.SpawnTowerPosition(input);
        Debug.Log("DistantTowerSpawner");
        return spawnedTower;
    }
}