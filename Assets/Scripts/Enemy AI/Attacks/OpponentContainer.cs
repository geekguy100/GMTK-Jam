using UnityEngine;

namespace EnemyAI
{
    public class OpponentContainer : MonoBehaviour
    {
        [SerializeField] private Transform opponent;

        public Transform GetOpponent()
        {
            return opponent;
        }
    }
}