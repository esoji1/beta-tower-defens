using System;
using UnityEngine;

public abstract class SpawnerTower
{
    private GameObject _prefabTower;
    private SpawnArea _clickedArea;
    private IMovable _movable;

    private GameObject _spawnedTower;

    public static event Action<GameObject> OnTowerSpawned;

    public SpawnerTower(IMovable movable, GameObject prefabTower, SpawnArea clickedArea)
    {
        _movable = movable;
        _prefabTower = prefabTower;
        _clickedArea = clickedArea;
    }

    public virtual GameObject SpawnTowerPosition(Vector3 input)
    {
        if (_clickedArea != null)
        {
            if (_clickedArea.CanSpawn())
            {
                _spawnedTower = UnityEngine.Object.Instantiate(_prefabTower, _clickedArea.transform.position, Quaternion.identity, null);
                _clickedArea.Occupy();

                CheckWhichTowerHasSpawned(_spawnedTower);

                Debug.Log("????? ??????? ??????????!");
                return _spawnedTower;
            }
            else
            {
                Debug.Log("??? ??????? ??? ??????.");
            }
        }
        return _spawnedTower;
    }

    private void CheckWhichTowerHasSpawned(GameObject tower)
    {
        if (tower.TryGetComponent(out MelleTower meleeTower))
        {
            OnTowerSpawned?.Invoke(tower);
        }
        else if (tower.TryGetComponent(out MageTower mageTower))
        {
            OnTowerSpawned?.Invoke(tower);
        }
        else if (tower.TryGetComponent(out DistanceTower distanceTower))
        {
            OnTowerSpawned?.Invoke(tower);
        }
        else
        {
            Debug.Log("??????????? ??? ?????.");
        }
    }
}