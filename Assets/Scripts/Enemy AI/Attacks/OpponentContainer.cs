using System;
using UnityEngine;

namespace EnemyAI
{
    public class OpponentContainer : MonoBehaviour
    {
        private EnemyStateManager opponentManager;
        [SerializeField] private EnvironmentObject opponent;

        private void Awake()
        {
            opponentManager = opponent.GetComponent<EnemyStateManager>();
        }

        public EnvironmentObject GetOpponent()
        {
            return opponent;
        }

        public EnemyStateManager GetOpponentStateManager()
        {
            return opponentManager;
        }
    }
}