using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemyAI.Attacks
{
    public class AttackBehaviourManager : MonoBehaviour
    {
        private AttackBehaviour currentAttack;
        private Dictionary<string, AttackBehaviour> attacks;
        private int NumAttacks => attacks.Count;

        [SerializeField] private OpponentContainer opponentContainer;

        private void Awake()
        {
            var components = GetComponents<AttackBehaviour>();
            attacks = new Dictionary<string, AttackBehaviour>();

            foreach (var component in components)
            {
                attacks.Add(component.GetName(), component);
            }
        }

        private void Start()
        {
            SetAttack(nameof(PunchAttackBehaviour));
        }

        public void SetAttack(string attackName)
        {
            if (!attacks.TryGetValue(attackName, out AttackBehaviour attack))
            {
                Debug.LogError("[AttackBehaviourManager]: Attack named " + attackName + " could not be found.");
                return;
            }

            currentAttack = attack;
        }

        private void SetRandomAttack()
        {
            int index = Random.Range(0, NumAttacks);
            currentAttack = attacks.Values.ElementAt(index);
        }

        public void PerformAttack()
        {
            currentAttack.PerformAttack(opponentContainer.GetOpponent());   
        }

        public bool IsMidAttack()
        {
            return currentAttack.IsMidAttack();
        }
    }
}