using UnityEngine;
using Zenject;

public class ClickOnSpawnPoint : MonoBehaviour, ITickable
{
    private MenuTowerVeiw _menuTowerVeiw;
    private Player _player;
    private RemoveTower _removeTower;
    private RadiusSpawnPoint _spawnRadius;

    private SpawnArea _lastClickedArea;
    private Tower _lastClickTower;
    private SpawnArea _clickedArea;
    private int _leftMouseButton = 0;

    public SpawnArea ClickedArea => _clickedArea;
    public MenuTowerVeiw MenuTowerVeiw => _menuTowerVeiw;

    [Inject]
    public void Construct(MenuTowerVeiw menuTowerVeiw, Player player, RemoveTower removeTower, RadiusSpawnPoint spawnRadius)
    {
        _menuTowerVeiw = menuTowerVeiw;
        _player = player;
        _removeTower = removeTower;
        _spawnRadius = spawnRadius;
    }

    public void Tick()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out SpawnArea spawnArea))
                {
                    RemovingSpawnedDummiesStage();

                    _clickedArea = spawnArea;

                    ToggleSpawnAreaMenu(spawnArea);
                }

                if (hit.collider.TryGetComponent(out Tower tower))
                    ToggleTowerMenu(tower);
            }
        }
    }

    private void RemovingSpawnedDummiesStage()
    {
        GameObject emptyObject = new GameObject();
        _removeTower.ChangePositionSpawnAndFlag(emptyObject, emptyObject.transform);
        Object.Destroy(emptyObject);
    }

    private void ToggleTowerMenu(Tower tower)
    {
        if (tower == _lastClickTower)
        {
            _menuTowerVeiw.OffTowerPumpingMenu();
            _lastClickTower = null;
            return;
        }

        _menuTowerVeiw.ShowTowerPumpingMenu(tower.transform.position);
        _lastClickTower = tower;

        _removeTower.ChangePositionSpawnAndFlag(tower.gameObject, tower.transform);
        _clickedArea = null;
    }

    private void ToggleSpawnAreaMenu(SpawnArea clickedArea)
    {
        if (clickedArea == _lastClickedArea)
        {
            _menuTowerVeiw.OffTowerBuildingMenu();
            _lastClickedArea = null;
            return;
        }

        _menuTowerVeiw.ShowTowerBuildingMenu(clickedArea.transform.position);
        _lastClickedArea = clickedArea;
    }
}