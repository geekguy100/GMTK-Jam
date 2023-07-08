using UnityEngine;

namespace EnemyAI
{
    /// <summary>
    /// 
    /// </summary>
    public class PursueDefendCheck : RngCheck
    {
        [SerializeField] private EnvironmentObject environmentObject;

        [SerializeField] private float minHealthPercent;
        [SerializeField] private float percentForFightingBelowMinHealth;
        [SerializeField] private float percentForFightingAboveMinHealth;

        protected override float PreparePassingPercent()
        {
            return environmentObject.GetHealthPercent() < minHealthPercent ? percentForFightingBelowMinHealth : percentForFightingAboveMinHealth;
        }

        protected override float GetRandomPercent()
        {
            return Random.Range(1, 101) / 100f;
        }
        
        public override string GetStateName()
        {
            return nameof(PursueDefendCheck);
        }

        public override void OnPursued()
        {
            throw new System.NotImplementedException();
        }

        public override void OnHit(object sender)
        {
            throw new System.NotImplementedException();
        }

        public override void OnStunned()
        {
            throw new System.NotImplementedException();
        }
    }
}