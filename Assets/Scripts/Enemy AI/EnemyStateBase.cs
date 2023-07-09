/*********************************
 * Author:          Kyle Grenier
 * Date Created:    07/07
 /********************************/

using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(EnemyStateManager))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EnemyStateBase : MonoBehaviour, IEnemyState
    {
        private Rigidbody2D rb;
        protected bool StateActive { get; private set; }
        protected EnemyStateManager StateManager { get; private set; }
        protected Obstacle PursuedStool { get; set; }

        protected virtual void Awake()
        {
            StateManager = GetComponent<EnemyStateManager>();
            rb = GetComponent<Rigidbody2D>();
        }

        public abstract void OnPursued();

        public virtual void OnHit(ref DamageData damageData)
        {
            return;
            
            damageData.force.y = 0;
            damageData.force.x = Mathf.Clamp(damageData.force.x, -10f, 10f);
            rb.AddForce(damageData.force, ForceMode2D.Impulse);
        }
        
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