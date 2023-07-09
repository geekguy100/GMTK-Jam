using UnityEngine;

namespace EnemyAI.Attacks
{
    public abstract class AttackBehaviour : MonoBehaviour
    {
        [SerializeField] protected AttackBehaviourData data;
        
        public abstract void PerformAttack(EnvironmentObject opponent, float? damage = null);
        public abstract bool IsMidAttack();
        public abstract string GetName();
    }
}