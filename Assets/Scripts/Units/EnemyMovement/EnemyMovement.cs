using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private NavMeshAgent _enemy;

    private void Start()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _enemy.updateRotation = false;
        _enemy.updateUpAxis = false;
    }

    private void Update()
    {
        _enemy.SetDestination(_target.position);
    }
}