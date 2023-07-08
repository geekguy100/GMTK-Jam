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
    [SerializeField] private TextSetter textSetter;
    [SerializeField] private bool hazardsReduceStamina = true;

    private void Awake()
    {
        stateManager = GetComponent<EnemyStateManager>();
        motor = GetComponent<PlayerMotor2D>();
        
        stamina = maxStamina;
    }
    
    public override void OnDamaged(DamageData data)
    {
        // Ignore all for Ground.
        if (data.sourceName == "Ground")
            return;
        
        // Applies knockback and reduces damage if in Defend state.
        stateManager.OnHit(ref data);
        
        // Only decrease health if receiving damage from another Fighter.
        if (data.sourceName == "Fighter")
        {
            base.OnDamaged(data);
        }
        else
        {
            Debug.Log(gameObject.name + " hit by " + data.sourceName);
        }
        
        if (stamina > 0)
        {
            bool reduceStamina = data.sourceName != "Hazard" || (hazardsReduceStamina && data.sourceName == "Hazard");
            
            if (reduceStamina && regenCoroutine != null)
            {
                StopCoroutine(regenCoroutine);
                regenCoroutine = null;
            }
            
            if (reduceStamina)
                stamina -= staminaLostPerHit;
            
            textSetter.SetStaminaText("Stamina: " + Mathf.Floor(stamina));
            if (stamina <= 0)
            {
                stateManager.SetState(nameof(DazedState));
            }
            
            if (reduceStamina)
                regenCoroutine = StartCoroutine(RegenStamina());
        }
        else if (regenCoroutine == null)
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
            
            textSetter.SetStaminaText("Stamina: " + Mathf.Floor(stamina));

            yield return null;
        }

        regenCoroutine = null;
    }
}