/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using System.Collections;
using EnemyAI.Attacks;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(AttackBehaviourManager))]
    public class AttackStoolState : EnemyStateBase
    {
        private AttackBehaviourManager attackManager;

        protected override void Awake()
        {
            base.Awake();
            attackManager = GetComponent<AttackBehaviourManager>();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            StartCoroutine(Attack());
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            print(gameObject.name + " no longer breaking the stool!");
            Debug.Break();
        }

        private IEnumerator Attack()
        {
            // Continually attack the stool until it breaks.
            while (PursuedStool != null)
            {
                attackManager.PerformAttack(PursuedStool);
                yield return new WaitWhile(() => attackManager.IsMidAttack());   
            }
            
            StateManager.SetState(nameof(PursueBackUpCheck));
        }

        public override void OnPursued()
        {
        }

        public override string GetStateName()
        {
            return nameof(AttackStoolState);
        }
    }
}