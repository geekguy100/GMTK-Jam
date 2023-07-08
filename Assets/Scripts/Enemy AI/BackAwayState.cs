﻿using System;
using EnemyAI.Data;
using KpattGames.Movement;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(PlayerMotor2D), typeof(OpponentContainer))]
    public class BackAwayState : EnemyStateBase
    {
        private PlayerMotor2D motor;
        private OpponentContainer opponentContainer;

        private Transform opponent;
        private Vector2 input;

        private float timeInState;

        [SerializeField] private Transform leftLedgeCheck;
        [SerializeField] private Transform rightLedgeCheck;
        [SerializeField] private float rayLength;
        [SerializeField] private BackAwayStateData data;
        [SerializeField] private LayerMask groundLayer;

        protected override void Awake()
        {
            base.Awake();
            motor = GetComponent<PlayerMotor2D>();
            opponentContainer = GetComponent<OpponentContainer>();
        }

        private void Start()
        {
            opponent = opponentContainer.GetOpponent().transform;
        }

        private void Update()
        {
            if (!StateActive)
                return;

            timeInState += Time.deltaTime;
            input = (transform.position - opponent.position).normalized;
        }

        private bool NearLedge()
        {
            bool groundedOnLeft = Physics2D.Raycast(leftLedgeCheck.position, -leftLedgeCheck.up, rayLength, groundLayer);
            bool groundedOnRight = Physics2D.Raycast(rightLedgeCheck.position, -rightLedgeCheck.up, rayLength, groundLayer);

            return !groundedOnLeft || !groundedOnRight;
        }

        private void FixedUpdate()
        {
            if (!StateActive) 
                return;
            
            motor.Move(input);
            
            if (Vector2.Distance(transform.position, opponent.position) >= data.MinDistance)
            {
                print("Distance check - fighters are " + data.MinDistance + " units apart at least.");
                StateManager.SetState(nameof(PursueDefendCheck));
            }
            else if (timeInState >= data.MaxAllowedTimeInState)
            {
                print("Time check - fighter has spent " + data.MaxAllowedTimeInState + " seconds backing up.");
                StateManager.SetState(nameof(PursueDefendCheck));
            }
            else if (NearLedge())
            {
                print("Near ledge, so no longer backing up.");
                StateManager.SetState(nameof(PursueDefendCheck));
            }
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            timeInState = 0f;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            input = Vector2.zero;
        }

        public override void OnPursued()
        {
        }

        public override void OnStunned()
        {
        }
        
        public override string GetStateName()
        {
            return nameof(BackAwayState);
        }

        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.yellow;
            var position = transform.position;
            UnityEditor.Handles.DrawLine(position, position + transform.right * data.MinDistance);
        }

        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawLine(leftLedgeCheck.position, leftLedgeCheck.position - leftLedgeCheck.up * rayLength);
            UnityEditor.Handles.DrawLine(rightLedgeCheck.position, rightLedgeCheck.position - rightLedgeCheck.up * rayLength);
        }
    }
}