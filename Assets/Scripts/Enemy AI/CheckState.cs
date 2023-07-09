using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyAI
{
    public abstract class CheckState : EnemyStateBase
    {
        [SerializeField] private EnemyStateBase passingState;
        [SerializeField] private EnemyStateBase failingState;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            StateManager.SetState(CheckPass() ? passingState : failingState);
        }

        protected abstract bool CheckPass();
    }
}