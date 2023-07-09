using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemyAI.Attacks
{
    [RequireComponent(typeof(OpponentContainer))]
    public class AttackBehaviourManager : MonoBehaviour
    {
        private AttackBehaviour currentAttack;
        private Dictionary<string, AttackBehaviour> attacks;
        private int NumAttacks => attacks.Count;

        private OpponentContainer opponentContainer;

        private void Awake()
        {
            opponentContainer = GetComponent<OpponentContainer>();
                
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

            SetAttack(attack);
        }

        public void SetAttack(AttackBehaviour attack)
        {
            currentAttack = attack;
        }

        private void SetRandomAttack()
        {
            int index = Random.Range(0, NumAttacks);
            SetAttack(attacks.Values.ElementAt(index));
        }

        public void PerformAttack(EnvironmentObject target, float? damage = null)
        {
            currentAttack.PerformAttack(target, damage);
        }

        public bool IsMidAttack()
        {
            return currentAttack.IsMidAttack();
        }
    }
}