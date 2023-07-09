using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(TextSetter))]
    public class EnemyStateManager : MonoBehaviour, IEnemyState
    {
        private bool HasCurrentState => !ReferenceEquals(currentState, null);
        private EnemyStateBase currentState;
        private Dictionary<string, EnemyStateBase> states;

        private TextSetter textSetter;

        private void Awake()
        {
            var stateComponents = GetComponents<EnemyStateBase>();
            states = new Dictionary<string, EnemyStateBase>();

            foreach (var component in stateComponents)
            {
                states.Add(component.GetStateName(), component);
            }

            textSetter = GetComponent<TextSetter>();
        }

        /// <summary>
        /// Performs an RNG check to see if the fighter should be in
        /// an Pursue or Defend state.
        /// </summary>
        private void Start()
        {
            SetState(nameof(PursueDefendCheck));
        }

        /// <summary>
        /// Sets the current state to the state with the provided name.
        /// </summary>
        /// <param name="stateName">The name of the state to set.</param>
        public void SetState(string stateName)
        {
            if (!states.TryGetValue(stateName, out EnemyStateBase state))
            {
                Debug.LogError("[EnemyStateManager]: Could not set the state named " + stateName);
                
                OnStateExit();
                currentState = null;
                return;
            }

            SetState(state);
        }

        public void SetState(EnemyStateBase state)
        {
            if (HasCurrentState)
                Debug.Log("[StateManager]: " + gameObject.name + " switching from " + currentState.GetStateName() + " to " + state.GetStateName());
            else
                Debug.Log("[StateManager]: Switching to " + state.GetStateName());

            OnStateExit();
            currentState = state;
            OnStateEnter();
        }

        #region IEnemyState implementations
        public void OnPursued()
        {
            if (HasCurrentState)
                currentState.OnPursued();
        }

        public void OnHit(ref DamageData damageData)
        {
            if (HasCurrentState)
                currentState.OnHit(ref damageData);
        }

        public string GetStateName()
        {
            return HasCurrentState ? currentState.GetStateName() : "Null";
        }

        public void OnStateEnter()
        {
            if (!HasCurrentState)
                return;
            
            textSetter.SetText(currentState.GetStateName());
            currentState.OnStateEnter();
        }

        public void OnStateExit()
        {
            if (!HasCurrentState)
                return;
            
            currentState.OnStateExit();
        }
        #endregion
    }
}