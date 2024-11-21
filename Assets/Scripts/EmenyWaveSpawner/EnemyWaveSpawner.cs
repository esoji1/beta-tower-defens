using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _timeBetweenWaves = 5f;

    [SerializeField] private Transform[] _targets;

    private int _currentWaveIndex = 0;
    private bool _isSpawning = false;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (_currentWaveIndex < _waves.Count)
        {
            if (!_isSpawning)
            {
                _isSpawning = true;
                yield return StartCoroutine(SpawnWave(_waves[_currentWaveIndex]));
                _isSpawning = false;

                _currentWaveIndex++;

                if (_currentWaveIndex < _waves.Count)
                {
                    yield return new WaitForSeconds(_timeBetweenWaves);
                }
            }
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        foreach (var group in wave.EnemyGroups)
        {
            for (int i = 0; i < group.Count; i++)
            {
                SpawnEnemy(group.EnemyPrefab);
                yield return new WaitForSeconds(group.SpawnInterval);
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, _spawnPoint.position, Quaternion.identity);

        if(enemy.TryGetComponent(out Orc orc))
        {
            orc.Initialize(_targets);
        }
        else if (enemy.TryGetComponent(out Werewolf werewolf))
        {
            werewolf.Initialize(_targets);
        }
        else if (enemy.TryGetComponent(out Archer archer))
        {
            archer.Initialize(_targets);
        }
        else
        {
            Debug.Log("такого врага нету");
        }
    }
}