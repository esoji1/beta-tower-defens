using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RemoveTower : MonoBehaviour
{
    [SerializeField] private GameObject _prefabFlag;

    private Button _buttonRemove;
    private ButtonsTowerSpawning _buttonsTowerSpawning;

    private Transform _pointSpawn;
    private List<GameObject> _inactiveFlags = new List<GameObject>();

    private void OnEnable()
    {
        _buttonRemove.onClick.AddListener(Remove);
    }

    private void OnDisable()
    {
        _buttonRemove.onClick.RemoveListener(Remove);
    }

    [Inject]
    public void Construct(ButtonsTowerSpawning buttonsTowerSpawning, [Inject(Id = "RemoveButton")] Button buttonRemove)
    {
        _buttonsTowerSpawning = buttonsTowerSpawning;
        _buttonRemove = buttonRemove;
    }

    public void ChangePositionSpawnAndFlag(GameObject item, Transform spawnPoint)
    {
        if (item == null || spawnPoint == null)
            return;

        _pointSpawn = spawnPoint;
        _buttonsTowerSpawning.SetLastSpawnTower(item);
    }

    private void Remove()
    {
        if (_buttonsTowerSpawning.LastSpawnTower != null)
        {
            Destroy(_buttonsTowerSpawning.LastSpawnTower);
            _buttonsTowerSpawning.ClickOnSpawnPoint.ClickedArea?.NotOccupy();

            _inactiveFlags.Add(Instantiate(_prefabFlag, _pointSpawn.position, Quaternion.identity, null));

            _buttonsTowerSpawning.ClickOnSpawnPoint.MenuTowerVeiw.OffTowerPumpingMenu();

            RemoveDisabledFlags();
        }
    }

    private void RemoveDisabledFlags()
    {
        for (int i = _inactiveFlags.Count - 1; i >= 0; i--)
        {
            if (_inactiveFlags[i].activeInHierarchy == false)
            {
                Destroy(_inactiveFlags[i]);
                _inactiveFlags.RemoveAt(i);
            }
        }
    }
}