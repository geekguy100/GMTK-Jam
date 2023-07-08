/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using UnityEngine;

namespace EnemyAI.Data
{
    [CreateAssetMenu(menuName = "State Data/Defend State Data")]
    public class DefendStateData : ScriptableObject
    {
        public float MovementFrequency => movementFrequency;
        public float MovementAmplitude => movementAmplitude;
        public float TimeInDefense => timeInDefense;
        public float PercentDamageBlockedWhileDefending => percentDamageBlockedWhileDefending;

        [SerializeField] private float movementFrequency;
        [SerializeField] private float movementAmplitude;
        [SerializeField] private float timeInDefense;
        [SerializeField][Range(0,1)] private float percentDamageBlockedWhileDefending;
    }
}