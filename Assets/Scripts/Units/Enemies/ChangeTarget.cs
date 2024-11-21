using System.Collections.Generic;
using UnityEngine;

namespace Units.Enemies
{
    public class ChangeTarget
    {
        public void SetNextTarget(List<Transform> targets, ref Transform currentTarget, ref bool isAttacking)
        {
            if (targets.Count > 0)
            {
                currentTarget = targets[0];
                isAttacking = false;
            }
            else
            {
                currentTarget = null;
            }
        }
    }
}