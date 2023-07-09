using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractableObject : MonoBehaviour, IInteractable
{
    public Rigidbody2D rb => GetComponent<Rigidbody2D>();

    public event Action OnInteractableDestroyed;

    private bool isAssigned = false;
    public bool IsAssigned => isAssigned;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }
    public virtual void OnAssigned()
    {
        rb.gravityScale = 0;
        isAssigned = true;
    }

    public virtual void OnUnassigned()
    {
        rb.gravityScale = 3;
        isAssigned = false;
    }

    void OnDestroy()
    {
        OnInteractableDestroyed?.Invoke();
    }
}