using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(OpponentContainer), typeof(EnvironmentObject))]
    public class HealthCheck : CheckState
    {
        private EnvironmentObject leftHandFighter;
        private EnvironmentObject rightHandFighter;
        [SerializeField] private float requiredHealthDiff;
        [SerializeField] private string stateName;

        protected override void Awake()
        {
            base.Awake();
            leftHandFighter = GetComponent<EnvironmentObject>();
            rightHandFighter = GetComponent<OpponentContainer>().GetOpponent();
        }

        protected override bool CheckPass()
        {
            return leftHandFighter.GetHealth() - rightHandFighter.GetHealth() >= requiredHealthDiff;
        }
        
        public override void OnPursued()
        {
            
        }

        public override string GetStateName()
        {
            return stateName;
        }
    }
}