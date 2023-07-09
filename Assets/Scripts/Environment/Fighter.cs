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
        if (data.sourceName == "Ground" || data.sourceName == "Untagged")
            return;
        
        // Reduces damage taken if in Defend or Backaway state.
        stateManager.OnHit(ref data);

        // Only decrease health if receiving damage from another Fighter.
        if (data.sourceName == "Fighter")
        {
            base.OnDamaged(data);
        }
        // Even though we do not decrease our fighter's health, we still want to
        // perform the behaviours when hit.
        else
        {
            if(data.sourceObject.GetComponent<Rigidbody2D>().velocity.magnitude > 15)
            {
                base.PerformBehaviours(data);
            }
            /*
            float mag = data.force.magnitude;
            
            mag -= DestructionConstants.MIN_DAMAGE_BUFFER;

            data.damage = mag;

            if (mag > 0)
            {
                base.PerformBehaviours(data);
            }
            */
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