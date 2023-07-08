using UnityEngine;

namespace EnemyAI.Attacks
{
    public class PunchAttackBehaviour : AttackBehaviour
    {
        private float timeOfAttack;
        
        [SerializeField] private float attackDuration;
        
        public override void PerformAttack(object opponent)
        {
            Debug.Log(gameObject.name + " Punches");
            timeOfAttack = Time.time;
        }

        public override bool IsMidAttack()
        {
            return (Time.time - timeOfAttack) < attackDuration;
        }

        public override string GetName()
        {
            return nameof(PunchAttackBehaviour);
        }
    }
}