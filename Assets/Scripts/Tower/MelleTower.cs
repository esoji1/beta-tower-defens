using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MelleTower : MonoBehaviour, IBaseTower
{
    private GameObject _warriorPrefab;

    private List<GameObject> _occupiedPositions;
    private int _timeBetweenSpawns = 0;
    private int _maximumNumberWarriors = 0;
    private int _count = 0;
    private List<Warrior> _warriors;
    private GameObject[] _positionPoints;
    private GameObject _warriorPositionPoints;

    public GameObject[] PositionPoints => _positionPoints;
    public  GameObject WarriorPositionPoints => _warriorPositionPoints;
    
    private void Awake()
    {
        _warriors = new List<Warrior>();
        _occupiedPositions = new List<GameObject>();
    }

    private void Start()
    {
        StartCoroutine(SpawnWarriors());
    }

    private void Update()
    {
        CleanUpNullWarriors();
    }

    public void Initialize(GameObject warriorPrefab, GameObject warriorPositionPoints)
    {
        _warriorPrefab = warriorPrefab;
        _timeBetweenSpawns = 2;
        _maximumNumberWarriors = 3;
        _warriorPositionPoints = Instantiate(warriorPositionPoints, transform);

        _positionPoints = _warriorPositionPoints
            .GetComponentsInChildren<Transform>()
            .Select(t => t.gameObject)
            .ToArray();
    }

    public void FindNewTarget()
    {
    }

    private IEnumerator SpawnWarriors()
    {
        while (true)
        {
            if (_count < _maximumNumberWarriors)
            {
                GameObject spawnPosition = GetNearestAvailablePosition();

                if (spawnPosition != null)
                {
                    yield return new WaitForSeconds(_timeBetweenSpawns);
                    _count++;
                    GameObject warrior = Instantiate(_warriorPrefab, transform);
                    _warriors.Add(warrior.GetComponent<Warrior>());
                    warrior.GetComponent<Warrior>().SetTarget(spawnPosition.transform);
                    _occupiedPositions.Add(spawnPosition);
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    private GameObject GetNearestAvailablePosition()
    {
        GameObject nearestPosition = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject position in _positionPoints)
        {
            if (!_occupiedPositions.Contains(position))
            {
                float distance = Vector3.Distance(transform.position, position.transform.position);
                
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestPosition = position;
                }
            }
        }

        return nearestPosition;
    }

    private void CleanUpNullWarriors()
    {
        for (int i = _warriors.Count - 1; i >= 0; i--)
        {
            if (_warriors[i] == null)
            {
                GameObject freedPosition = _occupiedPositions[i];
                _occupiedPositions.Remove(freedPosition);
                _warriors.RemoveAt(i);
                _count--;
            }
        }
    }
}