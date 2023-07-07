using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractableObject : MonoBehaviour, IInteractable
{
    Rigidbody2D IInteractable.rigidbody2D => GetComponent<Rigidbody2D>();

    public event Action OnInteractableDestroyed;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }

    public virtual void OnAssigned()
    {

    }

    public virtual void OnUnassigned()
    {

    }

    void OnDestroy()
    {
        OnInteractableDestroyed?.Invoke();
    }
}