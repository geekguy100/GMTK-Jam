/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using UnityEngine;

namespace EnemyAI.Data
{
    [CreateAssetMenu(menuName = "State Data/Pursue State Data")]
    public class PursueStateData : ScriptableObject
    {
        public float MinDistance => minDistance;
        [SerializeField] private float minDistance;
    }
}