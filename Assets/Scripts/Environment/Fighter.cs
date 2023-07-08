using EnemyAI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EnemyStateManager))]
public class Fighter : EnvironmentObject
{
    private EnemyStateManager stateManager;

    private void Awake()
    {
        stateManager = GetComponent<EnemyStateManager>();
    }
    
    public override void OnDamaged(DamageData data)
    {
        base.OnDamaged(data);
        stateManager.OnHit(data);
    }
}