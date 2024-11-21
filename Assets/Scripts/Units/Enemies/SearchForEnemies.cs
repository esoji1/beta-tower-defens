using System.Collections.Generic;
using UnityEngine;

namespace Units.Enemies
{
    public class SearchForEnemies
    {
        public void FindNewTargets(List<Transform> targets, Transform positionUnits, EnemyConfig config)
        {
            targets.Clear();
            
            Collider2D[] hitColliders =
                Physics2D.OverlapCircleAll(positionUnits.position, config.VisibilityRadius, config.LayerMask);

            foreach (var hitCollider in hitColliders)
                targets.Add(hitCollider.transform);
        }
    }
}