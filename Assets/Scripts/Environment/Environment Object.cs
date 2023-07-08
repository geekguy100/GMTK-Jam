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
                collisionForce *= DestructionConstants.DAMAGE_MODIFIER;
                break;
            default:
                break;
        }
        //force too weak to damage health
        collisionForce -= DestructionConstants.DAMAGE_BUFFER;
        if(collisionForce <= 0) { return; }

        string sourceName = collision.gameObject.tag;

        OnDamaged(new DamageData()
        {
            damage = collisionForce,
            force = collision.relativeVelocity,
            sourceName = sourceName
        });

        //Debug.Log(name + " hit with a force of " + collisionForce);
    }

    public virtual void OnDamaged(DamageData data)
    {
        if (invincible)
            return;
        
        health -= data.damage;
        if (!ReferenceEquals(healthText, null))
        {
            healthText.text = "HP: " + Mathf.Floor(health);
        }

        // Handle sound playing
        if(damageSounds.Count > 0)
        {
            AudioManager.Instance.PlayRandomClip(damageSounds);
        }
    }

    protected virtual void OnRemove()
    {
        OnObjectRemove?.Invoke();

        // Handle sound playing
        if(destructionSounds.Count > 0)
        {
            AudioManager.Instance.PlayRandomClip(destructionSounds);
        }
        
        if (destroyOnRemove)
            Destroy(gameObject);
    }
}
