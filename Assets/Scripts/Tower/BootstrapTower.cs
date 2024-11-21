using UnityEngine;

public class BootstrapTower : MonoBehaviour
{
    [SerializeField] private GameObject _warriorPrefab;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _warriorPositionPointsPrefab;

    private void OnEnable()
    {
        SpawnerTower.OnTowerSpawned += OnTowerSpawnedHandler;
    }

    private void OnDisable()
    {
        SpawnerTower.OnTowerSpawned += OnTowerSpawnedHandler;
    }

    private void OnTowerSpawnedHandler(GameObject tower)
    {
        if (tower.TryGetComponent(out MelleTower melleTower))
        {
            melleTower.Initialize(_warriorPrefab, _warriorPositionPointsPrefab);
        }
        else if (tower.TryGetComponent(out MageTower mageTower))
        {
            mageTower.Initialize(_layerMask);
        }
        else if (tower.TryGetComponent(out DistanceTower distanceTower))
        {
            distanceTower.Initialize(_layerMask);
        }
    }
}