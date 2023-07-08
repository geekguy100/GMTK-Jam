/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using UnityEngine;

namespace EnemyAI
{
    public abstract class RngCheck : EnemyStateBase
    {
        [SerializeField] private EnemyStateBase passingState;
        [SerializeField] private EnemyStateBase failingState;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            StateManager.SetState(CheckRng() ? passingState : failingState);
        }

        private bool CheckRng()
        {
            float passingPercent = PreparePassingPercent();
            return GetRandomPercent() >= (1 - passingPercent);
        }

        protected abstract float PreparePassingPercent();
        protected abstract float GetRandomPercent();
    }
}