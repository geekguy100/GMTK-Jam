using UnityEngine;

namespace EnemyAI.Attacks
{
    public class PunchAttackBehaviour : AttackBehaviour
    {
        public override void PerformAttack(object opponent)
        {
            Debug.Log(gameObject.name + " Punches");
        }

        public override bool IsMidAttack()
        {
            throw new System.NotImplementedException();
        }

        public override string GetName()
        {
            return nameof(PunchAttackBehaviour);
        }
    }
}