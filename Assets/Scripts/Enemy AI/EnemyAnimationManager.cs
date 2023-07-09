using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class EnemyAnimationManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator animator;

        private EnemyStateManager stateManager => GetComponent<EnemyStateManager>();

        // Start is called before the first frame update
        void Start()
        {
            stateManager.OnStateChanged += OnStateChanged;
        }

        void OnDestroy()
        {
            stateManager.OnStateChanged -= OnStateChanged;
        }

        void OnStateChanged(string stateName)
        {
            if(stateName.Contains("Idle"))
            {
                animator.SetTrigger("Idle");
            }
            else if(stateName.Contains("Attack"))
            {
                animator.SetTrigger("Attack");
            }
            else if(stateName.Contains("Defend"))
            {
                animator.SetTrigger("Defend");
            }
            else if(stateName.Contains("Dazed"))
            {
                animator.SetTrigger("Dazed");
            }
        }
    }
}
