using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractableObject : MonoBehaviour, IInteractable
{
    public Rigidbody2D rb => GetComponent<Rigidbody2D>();

    public event Action OnInteractableDestroyed;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }
    public virtual void OnAssigned()
    {
        rb.gravityScale = 0;
    }

    public virtual void OnUnassigned()
    {
        rb.gravityScale = 1;
    }

    void OnDestroy()
    {
        OnInteractableDestroyed?.Invoke();
    }
}