using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ButtonsTowerSpawning : MonoBehaviour
{
    private MeleeTowerPrefab _prefabMeleeTower;
    private MageTowerPrefab _prefabMiddleTower;
    private DistanceTowerPrefab _prefabDistatnceTower;
    private Player _player;
    private ClickOnSpawnPoint _clickOnSpawnPoint;
    private Button _buttonMiddleTower;
    private Button _buttonDistantTower;
    private Button _buttonMeleeTower;

    private GameObject _lastSpawnTower;
    private GameObject _towerSpawn;
    private TowerSpawnerStrategyFactory _factoryTowerSpawner;

    public GameObject LastSpawnTower => _lastSpawnTower;
    public ClickOnSpawnPoint ClickOnSpawnPoint => _clickOnSpawnPoint;

    private void OnEnable()
    {
        _buttonMiddleTower.onClick.AddListener(MiddleTowerButtonClicked);
        _buttonDistantTower.onClick.AddListener(DistantTowerButtonClicked);
        _buttonMeleeTower.onClick.AddListener(MeleeTowerButtonClicked);
    }

    private void OnDisable()
    {
        _buttonMiddleTower.onClick.RemoveListener(MiddleTowerButtonClicked);
        _buttonDistantTower.onClick.RemoveListener(DistantTowerButtonClicked);
        _buttonMeleeTower.onClick.RemoveListener(MeleeTowerButtonClicked);
    }

    [Inject]
    public void Construct(MeleeTowerPrefab prefabMeleeTower, MageTowerPrefab prefabMiddleTower,
        DistanceTowerPrefab prefabDistatnceTower, Player player, ClickOnSpawnPoint clickOnSpawnPoint,
        [Inject(Id = "MiddleTowerButton")] Button buttonMiddleTower,
        [Inject(Id = "DistantTowerButton")] Button buttonDistantTower,
        [Inject(Id = "MeleeTowerButton")] Button buttonMeleeTower)
    {
        _prefabMeleeTower = prefabMeleeTower;
        _prefabMiddleTower = prefabMiddleTower;
        _prefabDistatnceTower = prefabDistatnceTower;
        _player = player;
        _clickOnSpawnPoint = clickOnSpawnPoint;
        _buttonMiddleTower = buttonMiddleTower;
        _buttonDistantTower = buttonDistantTower;
        _buttonMeleeTower = buttonMeleeTower;

        _factoryTowerSpawner = new TowerSpawnerStrategyFactory(_player);
    }

    public void SetLastSpawnTower(GameObject lastSpawnTower)
        => _lastSpawnTower = lastSpawnTower;

    private void MiddleTowerButtonClicked()
    {
        SpawnTowerButtonClicked(TowerTypes.MageTower);
    }

    private void DistantTowerButtonClicked()
    {
        SpawnTowerButtonClicked(TowerTypes.DistanceTower);
    }

    private void MeleeTowerButtonClicked()
    {
        SpawnTowerButtonClicked(TowerTypes.MelleTower);
    }

    private void SpawnTowerButtonClicked(TowerTypes towerTypes)
    {
        _towerSpawn =
            _player.SetSpawnerTower(
                _factoryTowerSpawner.Get(towerTypes, _prefabMeleeTower, _prefabMiddleTower, _prefabDistatnceTower,
                    _clickOnSpawnPoint), Input.mousePosition);

        _clickOnSpawnPoint.ClickedArea?.gameObject.SetActive(false);
        _clickOnSpawnPoint.MenuTowerVeiw.OffTowerBuildingMenu();

        if (_towerSpawn != null)
            _lastSpawnTower = _towerSpawn;
    }
}