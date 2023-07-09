using UnityEngine;

namespace EnemyAI.Attacks
{
    public class PunchAttackBehaviour : AttackBehaviour
    {
        private float timeOfAttack;
        
        public override void PerformAttack(EnvironmentObject opponent, float? damage = null)
        {
            print(gameObject.name + " Punches");
            
            timeOfAttack = Time.time;

            // Set the damage to apply to the data's inherent damage if we are not
            // manually setting it.
            damage ??= data.Damage;

            opponent.OnDamaged(new DamageData()
            {
                damage = damage.Value,
                force = (opponent.transform.position - transform.position).normalized * data.Knockback,
                sourceName = gameObject.tag,
                sourceObject = gameObject
            });
        }

        public override bool IsMidAttack()
        {
            return (Time.time - timeOfAttack) < data.AttackDuration;
        }

        public override string GetName()
        {
            return nameof(PunchAttackBehaviour);
        }
    }
}