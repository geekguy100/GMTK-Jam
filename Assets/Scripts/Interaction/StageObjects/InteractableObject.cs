using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractableObject : MonoBehaviour, IInteractable
{

    public Rigidbody2D rigidbody2D { get { return GetComponent<Rigidbody2D>(); } }
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
}