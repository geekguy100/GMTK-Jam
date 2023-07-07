/*********************************
 * Author:          Kyle Grenier
 * Date Created:    
 /********************************/
using System.Collections;
using UnityEngine;

public class MouseInteractor : MonoBehaviour
{

    [Header("Current Mouse Settings")]
    public float mouseLerpSpeed = 0.1f;
    public float forcePower = 100f;
    private Vector3 previousInteractPos;

    /// <summary>
    /// The rate at which the mouse position is sampled.
    /// </summary>
    public float posSampleRateSeconds = 0.1f;

    private Coroutine sampleMousePosCoroutine;

    public IInteractable currentInteractable;

    private void Start()
    {
        sampleMousePosCoroutine = StartCoroutine(SampleMousePosition());
    }

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
                        currentInteractable.OnInteractableDestroyed -= OnInteractableDestroyed;
                    }

                    interactable.Interact();
                    currentInteractable = interactable;
                    currentInteractable.OnInteractableDestroyed += OnInteractableDestroyed;
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
                Vector3 force = currentInteractable.gameObject.transform.position - previousInteractPos;
                force *= forcePower;

                // Apply the force
                // currentInteractable.rigidbody2D.AddForce(force, ForceMode2D.Impulse);
                currentInteractable.rigidbody2D.velocity = force;

                currentInteractable.OnUnassigned();
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

        // Fix z component to some value
        mousePos.z = 10f;

        // Lerp towards mouse to give it a bit of force to throw
        currentInteractable.gameObject.transform.position = Vector3.Lerp(currentInteractable.gameObject.transform.position, mousePos, mouseLerpSpeed);
    }

    private void OnInteractableDestroyed()
    {
        currentInteractable = null;
    }

    private IEnumerator SampleMousePosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(posSampleRateSeconds);
            // previousInteractPos = currentInteractable.gameObject.transform.position;
            previousInteractPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}