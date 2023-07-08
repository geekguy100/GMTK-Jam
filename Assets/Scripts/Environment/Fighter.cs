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

    private void Awake()
    {
        stateManager = GetComponent<EnemyStateManager>();
        motor = GetComponent<PlayerMotor2D>();
        
        stamina = maxStamina;
    }
    
    public override void OnDamaged(DamageData data)
    {
        // Only decrease health if receiving damage from another Fighter.
        if (data.sourceName == "Fighter")
            base.OnDamaged(data);
        
        // Applies knockback.
        stateManager.OnHit(data);

        if (stamina > 0)
        {
            Debug.Log(gameObject.name + " STAMINA GREATER THAN 0", gameObject);
            if (regenCoroutine != null)
            {
                StopCoroutine(regenCoroutine);
                regenCoroutine = null;
            }
            
            stamina -= staminaLostPerHit;
            textSetter.SetStaminaText("Stamina: " + stamina);
            if (stamina <= 0)
            {
                stateManager.SetState(nameof(DazedState));
            }
            
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