/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using System;
using System.Collections;
using EnemyAI.Attacks;
using EnemyAI.Data;
using KpattGames.Movement;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(AttackBehaviourManager), typeof(PlayerMotor2D))]
    public class AttackStoolState : EnemyStateBase
    {
        private PlayerMotor2D motor;
        private AttackBehaviourManager attackManager;

        private Vector2 dir;
        
        [SerializeField] private float damageToStool;
        [SerializeField] private PursueStateData data;

        protected override void Awake()
        {
            base.Awake();
            attackManager = GetComponent<AttackBehaviourManager>();
            motor = GetComponent<PlayerMotor2D>();
        }

        public void SetStool(Obstacle stool)
        {
            PursuedStool = stool;
        }
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            StartCoroutine(Attack());
        }
        
        private void Update()
        {
            if (!StateActive)
                return;

            dir = (PursuedStool.transform.position - transform.position).normalized;
        }

        private void FixedUpdate()
        {
            if (!StateActive)
                return;
            
            motor.Move(dir);
        }

        private IEnumerator Attack()
        {
            // Continually attack the stool until it breaks.
            while (ShouldAttack())
            {
                attackManager.PerformAttack(PursuedStool, damageToStool);
                yield return new WaitWhile(attackManager.IsMidAttack);   
            }
            
            StateManager.SetState("PursueBackUpCheck");
        }

        private bool ShouldAttack()
        {
            return PursuedStool != null && Vector2.Distance(PursuedStool.transform.position, transform.position) <= data.MinDistance;
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