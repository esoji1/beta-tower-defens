using System;

public class TowerSpawnerStrategyFactory
{
    private Player _player;
    
    public TowerSpawnerStrategyFactory(Player player)
    {
        _player = player;
    }

    public SpawnerTower Get(TowerTypes towerTypes, MeleeTowerPrefab prefabMeleeTower, MageTowerPrefab prefabMageTower,
        DistanceTowerPrefab prefabDistatnceTower, ClickOnSpawnPoint clickOnSpawnPoint)
    {
        switch (towerTypes)
        {
            case TowerTypes.MelleTower:
                return new MeleeTowerSpawner(_player, prefabMeleeTower.GetMeleeTowerPrefab,
                    clickOnSpawnPoint.ClickedArea);

            case TowerTypes.MageTower:
                return new MageTowerSpawner(_player, prefabMageTower.GetMageTowerPrefab,
                    clickOnSpawnPoint.ClickedArea);

            case TowerTypes.DistanceTower:
                return new DistantTowerSpawner(_player, prefabDistatnceTower.GetDistanceTowerPrefab,
                    clickOnSpawnPoint.ClickedArea);
            
            default:
                throw new ArgumentException(nameof(towerTypes));
        }
    }
}