using System.Collections;
using EnemyAI;
using KpattGames.Movement;
using UnityEngine;

[RequireComponent(
    typeof(Rigidbody2D), 
    typeof(EnemyStateManager),
    typeof(PlayerMotor2D))]
public class Fighter : EnvironmentObject
{
    private EnemyStateManager stateManager;
    private Coroutine regenCoroutine;

    private PlayerMotor2D motor;
    
    private float stamina;
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaLostPerHit;
    [SerializeField] private float staminaRegenDelay;
    [SerializeField] private float staminaRegenMultiplier;

    private void Awake()
    {
        stateManager = GetComponent<EnemyStateManager>();
        motor = GetComponent<PlayerMotor2D>();
        
        stamina = maxStamina;
    }
    
    public override void OnDamaged(DamageData data)
    {
        base.OnDamaged(data);
        stateManager.OnHit(data);
        
        if (regenCoroutine != null)
            StopCoroutine(regenCoroutine);
        
        stamina -= staminaLostPerHit;
        if (stamina <= 0)
        {
            stateManager.SetState(nameof(DazedState));
        }
        else
        {
            regenCoroutine = StartCoroutine(RegenStamina());
        }
    }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(staminaRegenDelay);
        while (stamina < maxStamina)
        {
            stamina += staminaRegenMultiplier * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            
            yield return null;
        }
    }
}