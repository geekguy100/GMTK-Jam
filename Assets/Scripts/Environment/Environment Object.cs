using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Attached to any world object
/// </summary>
public class EnvironmentObject : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool invincible;


    private Rigidbody2D rigidBody;

    public event Action OnObjectRemove;

    // Start is called before the first frame update
    void Start()
    {
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


    protected virtual void CollisionHandler(Collision2D collision)
    {
        float collisionForce = collision.relativeVelocity.magnitude;
        collision.gameObject.GetComponent<EnvironmentObject>();
        
        switch (collision.gameObject.tag)
        {
            case "Debris":
                collisionForce *= DestructionConstants.DAMAGE_MODIFIER;
                break;
            default:
                break;
        }
        
        OnDamaged(collisionForce);

        //Debug.Log(name + " hit with a force of " + collisionForce);
    }

    protected virtual void OnDamaged(float damage)
    {
        health -= damage;
    }

    protected virtual void OnRemove()
    {
        OnObjectRemove?.Invoke();
        Destroy(gameObject);
    }
}
