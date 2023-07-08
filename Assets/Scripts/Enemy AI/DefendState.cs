using EnemyAI.Data;
using KpattGames.Movement;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(PlayerMotor2D))]
    public class DefendState : EnemyStateBase
    {
        private PlayerMotor2D motor;
        
        private float accumulatedAngle;
        private Vector2 dir;
        private float currentDefenseTime;

        [SerializeField] private DefendStateData data;

        protected override void Awake()
        {
            base.Awake();
            motor = GetComponent<PlayerMotor2D>();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            currentDefenseTime = 0f;
        }

        private void Update()
        {
            if (!StateActive)
                return;

            currentDefenseTime += Time.deltaTime;

            if (currentDefenseTime >= data.TimeInDefense)
            {
                StateManager.SetState(nameof(PursueBackUpCheck));
                return;
            }
            
            accumulatedAngle += Time.deltaTime;
            accumulatedAngle %= (Mathf.PI * 2);

            float targetSin = Mathf.Sin(accumulatedAngle * data.MovementFrequency) * data.MovementAmplitude;
            motor.Move((Vector2.right * targetSin) / motor.MotorData.movementSpeed);
        }

        public override void OnPursued()
        {
            
        }

        public override void OnStunned()
        {
        }
        

        public override string GetStateName()
        {
            return nameof(DefendState);
        }
    }
}