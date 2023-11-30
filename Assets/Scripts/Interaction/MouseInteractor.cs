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

    private float cachedTime;
    [SerializeField] private float maxHoldTime;

    private Camera mainCamera;

    private void Start()
    {
        sampleMousePosCoroutine = StartCoroutine(SampleMousePosition());
        mainCamera = Camera.main;
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

                    cachedTime = Time.time;

                    //currentInteractable.rb.velocity = Vector2.zero;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Unassign existing interactable if it exists.
            if (currentInteractable != null)
            {
                cachedTime = 0;
                print($"Unassigning interactable {currentInteractable}");

                currentInteractable.OnUnassigned();

                // Apply the force
                //currentInteractable.rb.AddForce(mouseVelocity * currentInteractable.rb.mass * mouseInteractData.forcePower, ForceMode2D.Impulse);
                //currentInteractable.rb.velocity = mouseVelocity * mouseInteractData.forcePower;
                //Debug.Log("MOUSE FORCE: " + mouseVelocity * currentInteractable.rb.mass * mouseInteractData.forcePower);
                currentInteractable.OnInteractableDestroyed -= OnInteractableDestroyed;
                currentInteractable = null;
            }
        }
    }

    private bool tickingDown;
    private void HandleInteractableMovement()
    {
        if (currentInteractable == null)
            return;

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);


        // Lerp towards mouse to give it a bit of force to throw
        //currentInteractable.rb.MovePosition(Vector2.Lerp(currentInteractable.rb.position, mousePos, mouseInteractData.mouseLerpSpeed * Time.deltaTime));
        
        Vector2 targetVelocity = (mousePos - currentInteractable.rb.position) * mouseInteractData.mouseFollowMultiplier;
        currentInteractable.rb.velocity = Vector2.Lerp(currentInteractable.rb.velocity, targetVelocity, mouseInteractData.mouseLerpSpeed * Time.deltaTime);

        if (Time.time - cachedTime >= maxHoldTime && !tickingDown)
        {
            tickingDown = true;
            StartCoroutine(TickHealthDown());
        }
    }
    
    private IEnumerator TickHealthDown()
    {
        var env = currentInteractable.gameObject.GetComponent<EnvironmentObject>();
        DamageData data = new DamageData()
        {
            damage = 10f,
            force = Vector2.zero,
            sourceName = "Player",
            sourceObject = gameObject
        };
        
        while (currentInteractable != null)
        {
            env.OnDamaged(data);
            yield return new WaitForSeconds(0.5f);
        }

        tickingDown = false;
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