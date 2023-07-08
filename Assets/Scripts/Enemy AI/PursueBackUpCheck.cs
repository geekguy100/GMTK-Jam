/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using EnemyAI.Data;
using UnityEngine;

namespace EnemyAI
{
    [RequireComponent(typeof(EnvironmentObject))]
    public class PursueBackUpCheck : RngCheck
    {
        private EnvironmentObject environmentObject;

        [SerializeField] private RngCheckData data;

        protected override void Awake()
        {
            base.Awake();
            environmentObject = GetComponent<EnvironmentObject>();
        }

        protected override float PreparePassingPercent()
        {
            return environmentObject.GetHealthPercent() < data.MinHealthPercent ? data.PercentForFightingBelowMinHealth : data.PercentForFightingAboveMinHealth;
        }

        protected override float GetRandomPercent()
        {
            return Random.Range(1, 101) / 100f;
        }
        
        public override void OnPursued()
        {
            throw new System.NotImplementedException();
        }

        public override string GetStateName()
        {
            return nameof(PursueBackUpCheck);
        }
    }
}