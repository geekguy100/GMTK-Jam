﻿using UnityEngine;

namespace EnemyAI.Attacks
{
    public class PunchAttackBehaviour : AttackBehaviour
    {
        private float timeOfAttack;
        
        public override void PerformAttack(EnvironmentObject opponent)
        {
            Debug.Log(gameObject.name + " Punches");
            
            timeOfAttack = Time.time;
            
            opponent.OnDamaged(new DamageData()
            {
                damage = data.Damage,
                force = (opponent.transform.position - transform.position).normalized * data.Knockback
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