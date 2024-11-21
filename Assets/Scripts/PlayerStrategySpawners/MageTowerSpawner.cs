using UnityEngine;

public class MageTowerSpawner : SpawnerTower
{
    private GameObject _prefabMageTower;
    private IMovable _movable;

    public MageTowerSpawner(IMovable movable, GameObject prefabTower, SpawnArea clickedArea) 
        : base(movable, prefabTower, clickedArea)
    {
    }

    public override GameObject SpawnTowerPosition(Vector3 input)
    {
        GameObject spawnedTower = base.SpawnTowerPosition(input);
        Debug.Log("MageTowerSpawner");
        return spawnedTower;
    }
}
