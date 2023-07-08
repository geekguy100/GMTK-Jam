using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Attached to any world object
/// </summary>
public class EnvironmentObject : MonoBehaviour
{
    public float MaxHealth { get; private set; }
    [SerializeField] private float health;
    [SerializeField] private bool invincible;


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
        if(collisionForce < DestructionConstants.DAMAGE_BUFFER) { return; }

        string sourceName = collision.gameObject.tag;
        if (sourceName == "Untagged")
            sourceName = "Ground";
        
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
    }

    protected virtual void OnRemove()
    {
        OnObjectRemove?.Invoke();
        Destroy(gameObject);
    }
}
