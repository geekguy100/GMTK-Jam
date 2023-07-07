using System;
using UnityEngine;

namespace EnemyAI.Attacks
{
    public abstract class AttackBehaviour : MonoBehaviour
    {
        public abstract void PerformAttack(object opponent);
        public abstract bool IsMidAttack();
        public abstract string GetName();
    }
}