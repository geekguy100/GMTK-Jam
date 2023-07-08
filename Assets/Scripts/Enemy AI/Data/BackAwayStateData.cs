/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using UnityEngine;

namespace EnemyAI.Data
{
    [CreateAssetMenu(menuName = "State Data/Back Away State Data")]
    public class BackAwayStateData : ScriptableObject
    {
        public float MinDistance => minDistance;
        public float MaxAllowedTimeInState => maxAllowedTimeInState;
        
        [SerializeField] private float minDistance;
        [SerializeField] private float maxAllowedTimeInState;
    }
}