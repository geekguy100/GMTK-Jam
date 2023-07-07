using System;
using System.Collections;
using KpattGames.Movement;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(PlayerMotor2D))]
    public class PursueState : EnemyStateBase
    {
        private PlayerMotor2D motor;
        private Vector2 input;
        private Transform opponent;
        
        [SerializeField] private OpponentContainer opponentContainer;
        
        protected override void Awake()
        {
            base.Awake();
            motor = GetComponent<PlayerMotor2D>();
        }

        private void Start()
        {
            opponent = opponentContainer.GetOpponent();
        }

        private void Update()
        {
            if (!StateActive)
                return;
            
            input = (opponent.position - transform.position).normalized;
        }

        private void FixedUpdate()
        {
            if (!StateActive)
                return;

            motor.Move(input);
        }

        public override void OnPursued()
        {
            print(gameObject.name + " is being pursued while he is pursuing.");
        }

        public override void OnStunned()
        {
            print(gameObject.name + " stunned while pursuing");
        }

        public override void OnHit(object sender)
        {
            print(gameObject.name + " got hit while pursuing.");
        }

        public override string GetStateName()
        {
            return nameof(PursueState);
        }
    }
}