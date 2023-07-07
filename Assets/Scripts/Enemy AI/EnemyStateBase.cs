/*********************************
 * Author:          Kyle Grenier
 * Date Created:    07/07
 /********************************/

using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(EnemyStateManager))]
    public abstract class EnemyStateBase : MonoBehaviour, IEnemyState
    {
        protected bool StateActive { get; private set; }
        protected EnemyStateManager StateManager { get; private set; }

        protected virtual void Awake()
        {
            StateManager = GetComponent<EnemyStateManager>();
        }

        public abstract void OnPursued();
        public abstract void OnStunned();
        public abstract void OnHit(object sender);
        public abstract string GetStateName();

        public virtual void OnStateEnter()
        {
            StateActive = true;
        }

        public virtual void OnStateExit()
        {
            StateActive = false;
        }
    }
}