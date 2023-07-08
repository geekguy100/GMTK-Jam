
using EnemyAI.Data;
using KpattGames.Movement;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(PlayerMotor2D), typeof(OpponentContainer))]
    public class PursueState : EnemyStateBase
    {
        private PlayerMotor2D motor;
        private Vector2 input;
        private Transform opponent;
        
        private OpponentContainer opponentContainer;
        [SerializeField] private PursueStateData data;
        
        protected override void Awake()
        {
            base.Awake();
            opponentContainer = GetComponent<OpponentContainer>();
            motor = GetComponent<PlayerMotor2D>();
        }

        private void Start()
        {
            opponent = opponentContainer.GetOpponent().transform;
        }

        /// <summary>
        /// Inform the opponent that they are being pursued.
        /// </summary>
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            opponentContainer.GetOpponentStateManager().OnPursued();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            motor.Move(Vector2.zero);
        }

        private void Update()
        {
            if (!StateActive)
                return;
            
            input = (opponent.position - transform.position).normalized;
            if (Vector2.Distance(transform.position, opponent.position) <= data.MinDistance)
            {
                StateManager.SetState(nameof(AttackState));
            }
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

        public override string GetStateName()
        {
            return nameof(PursueState);
        }

        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.back, data.MinDistance, 1f);
        }
    }
}