using System;
using KpattGames.Movement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyAI
{
    [RequireComponent(typeof(PlayerMotor2D))]
    public class DefendState : EnemyStateBase
    {
        private PlayerMotor2D motor;
        
        private float accumulatedAngle;
        private Vector2 dir;

        [SerializeField][Min(1f)] private float frequency;
        [SerializeField] private float maxMoveDistance;

        protected override void Awake()
        {
            base.Awake();
            motor = GetComponent<PlayerMotor2D>();
        }

        private void Update()
        {
            if (!StateActive)
                return;
            
            accumulatedAngle += Time.deltaTime;
            accumulatedAngle %= (Mathf.PI * 2);

            float targetSin = Mathf.Sin(accumulatedAngle * frequency) * maxMoveDistance;
            motor.Move((Vector2.right * targetSin) / motor.MotorData.movementSpeed);
        }

        public override void OnPursued()
        {
            
        }

        public override void OnStunned()
        {
        }

        public override void OnHit(object sender)
        {
            
        }

        public override string GetStateName()
        {
            return nameof(DefendState);
        }
    }
}