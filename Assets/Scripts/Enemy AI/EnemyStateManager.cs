using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemyAI
{
    public class EnemyStateManager : MonoBehaviour, IEnemyState
    {
        private EnemyStateBase currentState;
        private Dictionary<string, EnemyStateBase> states;

        private void Awake()
        {
            var stateComponents = GetComponents<EnemyStateBase>();
            states = new Dictionary<string, EnemyStateBase>();

            foreach (var component in stateComponents)
            {
                states.Add(component.GetStateName(), component);
            }
        }

        private void Start()
        {
            SetState(states.Keys.ElementAt(0));
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
                return;
            }
            
            OnStateExit();
            currentState = state;
            OnStateEnter();
        }

        /// <summary>
        /// Returns true if the current state's name matches the provided state name.
        /// </summary>
        /// <param name="stateName">The name of the state to check.</param>
        /// <returns>True if the current state's name matches the provided state name.</returns>
        public bool IsStateActive(string stateName)
        {
            if (ReferenceEquals(currentState, null))
                return false;
            
            return currentState.GetStateName() == stateName;
        }

        #region IEnemyState implementations
        public void OnPursued()
        {
            if (!ReferenceEquals(currentState, null))
                currentState.OnPursued();
        }

        public void OnHit(object sender)
        {
            if (!ReferenceEquals(currentState, null))
                currentState.OnHit(sender);
        }

        public string GetStateName()
        {
            return !ReferenceEquals(currentState, null) ? currentState.GetStateName() : "Null";
        }

        public void OnStateEnter()
        {
            if (!ReferenceEquals(currentState, null))
                currentState.OnStateEnter();
        }

        public void OnStateExit()
        {
            if (!ReferenceEquals(currentState, null))
                currentState.OnStateExit();
        }
        #endregion
    }
}