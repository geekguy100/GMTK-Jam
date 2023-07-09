using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(OpponentContainer), typeof(EnvironmentObject))]
    public class HealthCheck : RngCheck
    {
        [SerializeField] private bool isLeftFighter;

        private EnvironmentObject leftHandFighter;
        private EnvironmentObject rightHandFighter;
        [SerializeField] private float requiredHealthDiff;
        [SerializeField] private string stateName;
        [SerializeField] private bool compareHealthDirectly;

        [SerializeField] private float aboveMinPercent;
        [SerializeField] private float belowMinPercent;

        protected override void Awake()
        {
            base.Awake();
            leftHandFighter = GetComponent<EnvironmentObject>();
            rightHandFighter = GetComponent<OpponentContainer>().GetOpponent();
        }

        protected override float PreparePassingPercent()
        {
            float leftHealth = leftHandFighter.GetHealth();
            float rightHealth = rightHandFighter.GetHealth();
            if (isLeftFighter)
            {
                if (compareHealthDirectly)
                    return leftHealth > rightHealth ? aboveMinPercent : belowMinPercent;

                return leftHealth - rightHealth >= requiredHealthDiff ?
                        aboveMinPercent : belowMinPercent;
            }
            else
            {
                if (compareHealthDirectly)
                    return rightHealth > leftHealth ? aboveMinPercent : belowMinPercent;

                return rightHealth - leftHealth >= requiredHealthDiff ?
                        aboveMinPercent : belowMinPercent;
            }
            
        }
        
        // 40 - 10 = 30
        // Required diff is 10

        public override void OnPursued()
        {
            
        }

        public override string GetStateName()
        {
            return stateName;
        }
    }
}