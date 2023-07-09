using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

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


    protected Rigidbody2D rigidBody;
    private InteractableObject interactable => GetComponent<InteractableObject>();

    public event Action OnObjectRemove;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = health;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < DestructionConstants.MIN_Y_STAGE_BUFFER)
        {
            health = -1f;
        }
        
        if(health <= 0)
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
    
    protected virtual void CollisionHandler(Collision2D collision)
    {
        float collisionForce = collision.relativeVelocity.magnitude;
        
        switch (collision.gameObject.tag)
        {
            case "Debris":
                collisionForce *= DestructionConstants.PIECE_DAMAGE_MODIFIER;
                break;
            default:
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
        ModifyForce(ref appliedForce);

        OnDamaged(new DamageData()
        {
            damage = collisionForce,
            force = appliedForce,
            sourceName = sourceName
        });

        //Debug.Log(name + " hit with a force of " + collisionForce);
    }

    protected virtual void ModifyForce(ref Vector2 force)
    {
    }

    public virtual void OnDamaged(DamageData data)
    {
        if (invincible)
            return;

        switch (data.sourceName)
        {
            case "Ground":
                health -= data.damage * DestructionConstants.GROUND_DAMAGE_MULTIPLIER;
                Debug.Log("DAMAGE TO GROUND: " + data.damage);
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
    }

    protected virtual void OnRemove()
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
