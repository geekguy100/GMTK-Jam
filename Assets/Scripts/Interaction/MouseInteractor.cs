/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using System.Collections;
using UnityEngine;

public class MouseInteractor : MonoBehaviour
{
    [Header("Mouse Interact Settings")]
    public MouseInteractData mouseInteractData;

    private Vector2 prevMousePos;
    private Vector2 mouseVelocity;

    private Coroutine sampleMousePosCoroutine;

    public IInteractable currentInteractable;

    private void Start()
    {
        sampleMousePosCoroutine = StartCoroutine(SampleMousePosition());
    }

    public void Update()
    {
        HandleInput();
    }

    public void FixedUpdate()
    {
        HandleInteractableMovement();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
                 Vector2.zero,
                 100,
                 LayerMask.GetMask("Interactable")); 
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.transform.parent.gameObject;
                print($"Hit {hitObject.name}");
                
                IInteractable interactable = hitObject.GetComponent<IInteractable>();
                if (interactable != null && interactable != currentInteractable)
                {
                    // Unassign existing interactable if it exists.
                    if(currentInteractable != null)
                    {
                        currentInteractable.OnUnassigned();
                        currentInteractable.OnInteractableDestroyed -= OnInteractableDestroyed;
                    }

                    interactable.Interact();
                    interactable.OnAssigned();
                    currentInteractable = interactable;
                    currentInteractable.OnInteractableDestroyed += OnInteractableDestroyed;

                    currentInteractable.rb.velocity = Vector2.zero;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Unassign existing interactable if it exists.
            if (currentInteractable != null)
            {
                print($"Unassigning interactable {currentInteractable}");

                currentInteractable.OnUnassigned();

                // Apply the force
                currentInteractable.rb.AddForce(mouseVelocity * currentInteractable.rb.mass * mouseInteractData.forcePower, ForceMode2D.Impulse);
                currentInteractable.OnInteractableDestroyed -= OnInteractableDestroyed;
                currentInteractable = null;
            }
        }
    }

    private void HandleInteractableMovement()
    {
        if (currentInteractable == null)
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        // Lerp towards mouse to give it a bit of force to throw
        currentInteractable.rb.MovePosition(Vector2.Lerp(currentInteractable.rb.position, mousePos, mouseInteractData.mouseLerpSpeed * Time.deltaTime));
    }

    private void OnInteractableDestroyed()
    {
        currentInteractable = null;
    }

    /// <summary>
    /// Samples the previous mouse position every mouseInteractData.posSampleRateSeconds seconds. This previous position is used to calculate the force to apply to the interactable.
    /// </summary>
    private IEnumerator SampleMousePosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(mouseInteractData.posSampleRateSeconds);
            prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float mouseX = Input.GetAxisRaw("Mouse X");
            float mouseY = Input.GetAxisRaw("Mouse Y");
            mouseVelocity = new Vector2(mouseX, mouseY);
        }
    }
}