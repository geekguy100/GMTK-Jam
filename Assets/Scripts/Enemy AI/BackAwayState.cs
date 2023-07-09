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
        
        [SerializeField] private BackAwayStateData data;

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
        
        public override void OnHit(ref DamageData damageData)
        {
            damageData.damage -= damageData.damage * (1-data.PercentDamageBlockedWhileDefending);
            base.OnHit(ref damageData);
        }

        private void FixedUpdate()
        {
            if (!StateActive) 
                return;
            
            motor.Move(input);
            
            if (Vector2.Distance(transform.position, opponent.position) >= data.MinDistance)
            {
                print("Distance check - fighters are " + data.MinDistance + " units apart at least.");
                StateManager.SetState("PursueDefendCheck");
            }
            else if (timeInState >= data.MaxAllowedTimeInState)
            {
                print("Time check - fighter has spent " + data.MaxAllowedTimeInState + " seconds backing up.");
                StateManager.SetState("PursueDefendCheck");
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

        public override string GetStateName()
        {
            return nameof(BackAwayState);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.yellow;
            var position = transform.position;
            UnityEditor.Handles.DrawLine(position, position + transform.right * data.MinDistance);
        }
        #endif
    }
}