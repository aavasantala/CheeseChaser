using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MouseInteractor : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public UIManager uiManager; // optional, for crosshair updates

    [Header("Camera during interaction")]
    public Vector3 cameraOffset = new Vector3(0, 1.0f, -2f); // behind the player
    public float cameraFollowSpeed = 5f; // smooth follow
    public float cameraLookSpeed = 5f;   // smooth look at target

    [Header("Interaction Settings")]
    public float interactRange = 2.5f;
    public float pushPullSpeed = 2f;
    public float interactBreakDistance = 4f;
    public LayerMask interactMask;

    private CharacterController controller;
    private MouseController movementScript;

    private Rigidbody currentRb;
    private Transform currentTarget;
    private bool isInteracting = false;
    private bool isSmall = true;

    // Camera original position & rotation for restoring after interaction
    private Vector3 originalCamLocalPos;
    private Quaternion originalCamLocalRot;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        movementScript = GetComponent<MouseController>();
    }

    void Update()
    {
        isSmall = movementScript != null && movementScript.IsSmall();

        if (isSmall)
        {
            if (uiManager != null) uiManager.SetCanInteract(false);
            return;
        }

        if (isInteracting)
        {
            RotatePlayerTowardsInteractable();
            HandleActiveInteraction();
            HandleInteractionCamera();
        }
        else
        {
            HandleInteractionRaycast();
        }
    }

    #region Interaction Raycast
    void HandleInteractionRaycast()
    {
        bool canInteract = false;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactMask))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                canInteract = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartInteraction(hit);
                    return;
                }
            }
        }

        if (uiManager != null)
            uiManager.SetCanInteract(canInteract);
    }

    void StartInteraction(RaycastHit hit)
    {
        isInteracting = true;
        currentTarget = hit.collider.transform;
        currentRb = hit.collider.attachedRigidbody;

        if (currentRb != null)
        {
            currentRb.angularVelocity = Vector3.zero;
            currentRb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        // Save original camera position & rotation
        originalCamLocalPos = playerCamera.transform.localPosition;
        originalCamLocalRot = playerCamera.transform.localRotation;

        movementScript.SetLookEnabled(false);

        if (uiManager != null)
            uiManager.SetCanInteract(false);
    }
    #endregion

    #region Active Interaction
    void HandleActiveInteraction()
    {
        if (currentTarget == null)
        {
            StopInteraction();
            return;
        }

        float distance = Vector3.Distance(transform.position, currentTarget.position);
        if (distance > interactBreakDistance)
        {
            StopInteraction();
            return;
        }

        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = (currentTarget.position - transform.position).normalized;

        // Move object
        if (currentRb != null && Mathf.Abs(vertical) > 0.01f)
        {
            Vector3 targetVelocity = dir * vertical * pushPullSpeed;
            targetVelocity.y = currentRb.linearVelocity.y; // preserve gravity
            currentRb.linearVelocity = targetVelocity;

            // Move mouse with object
            controller.Move(dir * vertical * Time.deltaTime * pushPullSpeed);
        }

        // Stop interaction manually
        if (Input.GetKeyDown(KeyCode.E))
        {
            StopInteraction();
        }
    }
    #endregion

    #region Camera Control
    void HandleInteractionCamera()
    {
        if (currentTarget == null) return;

        // Camera behind the player, always keeping player between camera and object
        Vector3 toInteractable = currentTarget.position - transform.position;
        Vector3 desiredPosition = transform.position - toInteractable.normalized * Mathf.Abs(cameraOffset.z)
                                  + Vector3.up * cameraOffset.y;
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, desiredPosition, Time.deltaTime * cameraFollowSpeed);

        // Camera looks at the interactable
        Vector3 lookTarget = currentTarget.position + Vector3.up * 0.5f;
        Vector3 direction = (lookTarget - playerCamera.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, targetRotation, Time.deltaTime * cameraLookSpeed);
    }

    void RotatePlayerTowardsInteractable()
    {
        if (currentTarget == null) return;

        Vector3 lookDir = currentTarget.position - transform.position;
        lookDir.y = 0; // only rotate on the horizontal plane
        if (lookDir.sqrMagnitude < 0.001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }
    #endregion

    void StopInteraction()
    {
        isInteracting = false;
        currentTarget = null;

        if (currentRb != null)
        {
            currentRb.linearVelocity = Vector3.zero;
            currentRb.constraints = RigidbodyConstraints.None;
        }
        currentRb = null;

        movementScript.SetLookEnabled(true);

        // Restore camera
        playerCamera.transform.localPosition = originalCamLocalPos;
        playerCamera.transform.localRotation = originalCamLocalRot;
    }
}
