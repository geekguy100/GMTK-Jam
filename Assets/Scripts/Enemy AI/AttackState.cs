using System.Collections;
using EnemyAI.Attacks;
using UnityEngine;

namespace EnemyAI
{
    public class AttackState : EnemyStateBase
    {
        [SerializeField] private AttackBehaviourManager attackManager;

        public override void OnStateEnter()
        {
            StartCoroutine(Attack());
        }

        public override void OnStateExit()
        {
            StopAllCoroutines();
        }
        
        public override void OnPursued()
        {
            print(gameObject.name + " is being pursued while attacking.");
        }

        public override void OnStunned()
        {
            print(gameObject.name + " stunned while attacking!");
        }

        private IEnumerator Attack()
        {
            while (StateManager.IsStateActive(nameof(AttackState)))
            {
                yield return new WaitWhile(() => attackManager.IsMidAttack());
                attackManager.PerformAttack();
            }
        }

        public override void OnHit(object sender)
        {
            print(gameObject.name + " hit while attacking");
        }

        public override string GetStateName()
        {
            return nameof(AttackState);
        }
    }
}