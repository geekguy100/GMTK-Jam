/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
namespace Interaction
{
    public interface IInteractable
    {
        void Interact();
        void OnAssigned();
        void OnUnassigned();
    }   
}