/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using UnityEngine;
public interface IInteractable
{
    void Interact();
    void OnAssigned();
    void OnUnassigned();

    GameObject gameObject { get; }

    Rigidbody2D rigidbody2D { get; }
}   