using UnityEngine;

namespace EnemyAI
{
    public class EnemyRngCheck : MonoBehaviour
    {
        [SerializeField] private MinMax defenseWaitTimes;

        public bool GetDefenseCheck()
        {
            return true;
        }
    }
}