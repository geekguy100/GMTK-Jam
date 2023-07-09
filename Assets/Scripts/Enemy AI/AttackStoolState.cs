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
        [SerializeField] private float damageToStool;

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

        private IEnumerator Attack()
        {
            // Continually attack the stool until it breaks.
            while (PursuedStool != null)
            {
                attackManager.PerformAttack(PursuedStool, damageToStool);
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