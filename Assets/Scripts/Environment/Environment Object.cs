using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Attached to any world object
/// </summary>
public class EnvironmentObject : MonoBehaviour
{
    public float MaxHealth { get; private set; }
    [SerializeField] private float health;
    [SerializeField] private bool invincible;
    [SerializeField] private bool destroyOnRemove = true;
    [SerializeField] private TextMeshProUGUI healthText;


    [Header("Audio")]
    [SerializeField] private List<AudioClip> damageSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> destructionSounds = new List<AudioClip>();

    private IDamageableBehaviour[] behaviours;


    protected Rigidbody2D rigidBody;
    private InteractableObject interactable => GetComponent<InteractableObject>();

    public event Action OnObjectRemove;
    public event Action<float> OnHealthAdded;
    
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = health;
        rigidBody = GetComponent<Rigidbody2D>();
        behaviours = GetComponents<IDamageableBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < DestructionConstants.MIN_Y_STAGE_BUFFER)
        {
            OnRemove();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionHandler(collision);
    }

    public float GetHealthPercent()
    {
        return (health / MaxHealth);
    }

    public void AddHealth(float amount)
    {
        float prevHealth = health;
        
        health += amount;
        health = Mathf.Clamp(health, 0, MaxHealth);

        // Figure out exactly how much health we wound up adding.
        // If the player has 95 health and we add 10, we set the amount to 5 
        // because that's the actual amount we added.
        float diff = prevHealth - health;
        if (diff < amount)
        {
            amount = diff;
        }
        
        OnHealthAdded?.Invoke(amount);
    }
    
    protected virtual void CollisionHandler(Collision2D collision)
    {
        float collisionForce = collision.relativeVelocity.magnitude;
        
        switch (collision.gameObject.tag)
        {
            case "Debris":
                collisionForce *= DestructionConstants.PIECE_DAMAGE_MODIFIER;
                break;
        }
        
        //force too weak to damage health
        collisionForce -= DestructionConstants.MIN_DAMAGE_BUFFER;
        if(collisionForce <= 0) { return; }

        if (interactable != null && interactable.IsAssigned)
        {
            collisionForce *= DestructionConstants.INTERACT_DAMAGE_REDUCTION_MULTIPLIER;
        }
        
        string sourceName = collision.gameObject.tag;
        
        Vector2 appliedForce = collision.relativeVelocity;

        OnDamaged(new DamageData()
        {
            damage = collisionForce,
            force = appliedForce,
            sourceName = sourceName,
            sourceObject = collision.gameObject
        });

        //Debug.Log(name + " hit with a force of " + collisionForce);
    }

    public virtual void OnDamaged(DamageData data)
    {
        if (invincible)
            return;

        switch (data.sourceName)
        {
            case "Ground":
                health -= data.damage * DestructionConstants.GROUND_DAMAGE_MULTIPLIER;
                //Debug.Log("DAMAGE TO GROUND: " + data.damage);
                break;
            default:
                health -= data.damage;
                break;
        }
        
        if (!ReferenceEquals(healthText, null))
        {
            healthText.text = "HP: " + Mathf.Floor(health);
        }

        // Handle sound playing
        if(damageSounds.Count > 0)
        {
            AudioManager.Instance.PlayRandomObjectClip(damageSounds);
        }

        PerformBehaviours(data);
        
        if (health <= 0)
            OnRemove();
    }

    public void PerformBehaviours(DamageData data)
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.PerformBehaviour(data);
        }
    }

    public virtual void OnRemove()
    {
        OnObjectRemove?.Invoke();

        // Handle sound playing
        if(destructionSounds.Count > 0)
        {
            AudioManager.Instance.PlayRandomObjectClip(destructionSounds);
        }
        
        if (destroyOnRemove)
            Destroy(gameObject);
    }
}