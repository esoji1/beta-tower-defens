using UnityEngine;

public class Arrow : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed = 10f;
    private Transform target;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            Damage(target, 20);
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    public void Damage(Transform enemyTransform, int damage)
    {
        if (enemyTransform.TryGetComponent(out IDamage unit))
        {
            if (unit.Health.HealthValue > 0)
            {
                unit.Damage(damage); 
            }
            else
            {
                Destroy(enemyTransform.gameObject);
            }
        }

        Destroy(gameObject);
    }

    public void SetTarget(Transform newTarget) => target = newTarget;
}