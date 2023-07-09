
using System;
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

        // TODO: May need to add this back.
        // public override void OnStateExit()
        // {
        //     base.OnStateExit();
        //     motor.Move(Vector2.zero);
        // }

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

        /// <summary>
        /// Caches the incoming object if it is a Stool.
        /// </summary>
        /// <param name="other">The Collider which entered the trigger.</param>
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (StateManager.GetStateName() == nameof(DazedState))
                return;
            
            if (other.gameObject.CompareTag("Stool"))
            {
                PursuedStool = other.gameObject.GetComponent<Obstacle>();

                if (!TransformHelper.IsObjectBehind(transform, opponent.transform, PursuedStool.transform))
                {
                    var state = StateManager.GetState<AttackStoolState>(nameof(AttackStoolState));
                    state.SetStool(PursuedStool);

                    StateManager.SetState(state);
                }
            }
        }

        /// <summary>
        /// Sets the cached Stool to null if it was a part of the exiting Collision.
        /// </summary>
        /// <param name="other">The exiting Collision.</param>
        private void OnCollisionExit2D(Collision2D other)
        {
            if (PursuedStool != null && other.gameObject == PursuedStool.gameObject)
            {
                PursuedStool = null;
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