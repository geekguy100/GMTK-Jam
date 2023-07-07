/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using UnityEngine;

public class MouseInteractor : MonoBehaviour
{

    [Header("Current Mouse Settings")]
    public float mouseLerpSpeed = 0.1f;
    public float forcePower = 100f;
    private Vector3 previousInteractablePos;



    public IInteractable currentInteractable;

    public void Update()
    {
        HandleInput();

        HandleInteractableMovement();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
                 Vector2.zero); 
            if (hit.collider != null)
            {
                print($"Hit {hit.collider.gameObject.name}");
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null && interactable != currentInteractable)
                {
                    // Unassign existing interactable if it exists.
                    if(currentInteractable != null)
                    {
                        currentInteractable.OnUnassigned();
                    }

                    interactable.Interact();
                    currentInteractable = interactable;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Unassign existing interactable if it exists.
            if (currentInteractable != null)
            {
                print($"Unassigning interactable {currentInteractable}");

                // Calculate the force to apply to the interactabl
                Vector3 force = currentInteractable.gameObject.transform.position - previousInteractablePos;
                force *= forcePower;

                // Apply the force
                currentInteractable.rigidbody2D.AddForce(force, ForceMode2D.Impulse);

                currentInteractable.OnUnassigned();
                currentInteractable = null;
            }
        }
    }

    private void HandleInteractableMovement()
    {
        if (currentInteractable == null)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Fix z component to some value
        mousePos.z = 10f;

        // Store previous position
        previousInteractablePos = currentInteractable.gameObject.transform.position;

        // Lerp towards mouse to give it a bit of force to throw
        currentInteractable.gameObject.transform.position = Vector3.Lerp(currentInteractable.gameObject.transform.position, mousePos, mouseLerpSpeed);
    }
}