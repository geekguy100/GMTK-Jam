/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using UnityEngine;

namespace Interaction
{
    public class Interactor : MonoBehaviour
    {
        private IInteractable assignedInteractable;

        public void AssignInteractable(IInteractable interactable)
        {
            if (assignedInteractable != null)
            {
                UnassignInteractable();
            }

            assignedInteractable = interactable;
            assignedInteractable.OnAssigned();
        }

        public void Interact()
        {
            assignedInteractable?.Interact();
        }

        public void UnassignInteractable()
        {
            assignedInteractable.OnUnassigned();
            assignedInteractable = null;
        }
    }
}