/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/

using System;
using UnityEngine;
public interface IInteractable
{
    void Interact();
    void OnAssigned(); 
    void OnUnassigned();
    
    event Action OnInteractableDestroyed;

    GameObject gameObject { get; }

    Rigidbody2D rb { get; }
}
