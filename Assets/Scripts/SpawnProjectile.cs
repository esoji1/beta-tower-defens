using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public Transform SpawnArrow;

    private void Awake()
        => SpawnArrow = transform;
}