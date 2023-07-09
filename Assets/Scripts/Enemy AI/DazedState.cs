using System.Collections;
using KpattGames.Movement;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(PlayerMotor2D))]
    public class DazedState : EnemyStateBase
    {
        private PlayerMotor2D motor;
        [SerializeField] private float recoveryTime;

        protected override void Awake()
        {
            base.Awake();
            motor = GetComponent<PlayerMotor2D>();
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            print(gameObject.name + " is Dazed!");
            motor.Move(Vector2.zero);
            motor.Deactivate(false);
            StartCoroutine(Recover());
        }

        private IEnumerator Recover()
        {
            yield return new WaitForSeconds(recoveryTime);
            print(gameObject.name + " has recovered!");
            
            motor.Activate();
            StateManager.SetState(nameof(PursueDefendCheck));
        }

        public override void OnPursued()
        {
        }

        public override string GetStateName()
        {
            return nameof(DazedState);
        }
    }
}