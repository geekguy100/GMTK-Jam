using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// Attached to any world object
/// </summary>
public class EnvironmentObject : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool invincible;


    [Header("Audio")]
    [SerializeField] private List<AudioClip> damageSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> destructionSounds = new List<AudioClip>();


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
        Destroy(gameObject);
    }
}
