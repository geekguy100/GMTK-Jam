﻿using System.Collections;
using EnemyAI.Attacks;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(AttackBehaviourManager), typeof(OpponentContainer))]
    public class AttackState : EnemyStateBase
    {
        private AttackBehaviourManager attackManager;
        private OpponentContainer opponentContainer;

        protected override void Awake()
        {
            base.Awake();
            attackManager = GetComponent<AttackBehaviourManager>();
            opponentContainer = GetComponent<OpponentContainer>();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            StartCoroutine(Attack());
        }
        
        private IEnumerator Attack()
        {
            // Perform the attack and wait for it to complete.
            // Once the attack is done, switch back to the PursueDefendCheck state to see
            // if we should continue attacking or go on the defense.
            attackManager.PerformAttack(opponentContainer.GetOpponent());
            
            yield return new WaitWhile(() => attackManager.IsMidAttack());
            
            StateManager.SetState("PursueDefendCheck");
        }

        public override void OnPursued()
        {
            print(gameObject.name + " is being pursued while attacking.");
        }

        public override string GetStateName()
        {
            return nameof(AttackState);
        }
    }
}