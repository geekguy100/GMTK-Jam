/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using UnityEngine;

namespace EnemyAI.Data
{
    [CreateAssetMenu(menuName = "State Data/Rng Check Data")]
    public class RngCheckData : ScriptableObject
    {
        public float MinHealthPercent => minHealthPercent;
        public float PercentForFightingBelowMinHealth => percentForFightingBelowMinHealth;
        public float PercentForFightingAboveMinHealth => percentForFightingAboveMinHealth;

        [SerializeField] private float minHealthPercent;
        [SerializeField] private float percentForFightingBelowMinHealth;
        [SerializeField] private float percentForFightingAboveMinHealth;
    }
}