using UnityEngine;

namespace EnemyAI.Attacks
{
    [CreateAssetMenu(menuName = "Attacks/Attack Data")]
    public class AttackBehaviourData : ScriptableObject
    {
        public float AttackDuration => attackDuration;
        public float Damage => Random.Range(damage.min, damage.max);
        public float Knockback => Random.Range(knockback.min, knockback.max);
        
        [SerializeField] private float attackDuration;
        [SerializeField] private MinMax damage;
        [SerializeField] private MinMax knockback;
    }
}