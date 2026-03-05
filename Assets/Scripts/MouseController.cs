using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MouseController : MonoBehaviour
{
    //idk about these settings yet, need to tweak
    [Header("Settings for movement")]
    [SerializeField] private float smallMouseSpeed = 6f;
    [SerializeField] private float bigMouseSpeed = 3f;
    [SerializeField] private float smallMouseJumpHeight = 1f;
    [SerializeField] private float gravity = -9.81f;
    //idk what this does ^^

    //size tings
    [Header("Settings for mouse size")]
    [SerializeField] private Vector3 smallSize = new Vector3(0.25f, 0.25f, 0.25f);
    [SerializeField] private Vector3 bigSize = new Vector3(0.40f, 0.80f, 0.40f);

    [Header("Mouse Look Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity = 150f;
    [SerializeField] private float cameraYOffsetSmall = 0.4f;
    [SerializeField] private float cameraYOffsetBig = 1f;

    //same with this idk what this does ^^
    private float xRotation = 0f;

    //some stuff, isgrounded makes it so that you can't jump mid air etc. velocity is for movement
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSmall = true;
    private Camera mainCam;
    private bool canLook = true;
    private bool isPull = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();

        LevelManager.Instance.onMushroomTaken += OnMushroomTaken;
        LevelManager.Instance.onBoxInRightPlace += OnBoxInRightPlace;
        LevelManager.Instance.onPlayerOutOfZone += OnPlayerOutOfZone;
        
        SizeToSmall(true);
    }


    void Update()
    {
        CameraHandler();
        MovementHandler();
    }


    void CameraHandler()
    {
        if (!canLook) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    
    void MovementHandler()
    {
        //is mouse on ground?
        isGrounded = characterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //movement directions
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        //gets speed based on if mouse is small or big
        float speed = isSmall ? smallMouseSpeed : bigMouseSpeed;

        characterController.Move(move * (speed * Time.deltaTime));

        //jumping
        if (isSmall && Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(smallMouseJumpHeight * -2f * gravity);
        }

        if (characterController.collisionFlags == CollisionFlags.Above)
        {
            velocity.y = -1f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity*Time.deltaTime);
    }
    
    //This is the method that changes the size of the mouse
    //Usable with SizeToSmall(true) -> that then makes the mouse small, SizeToSmall(false) for big mouse.
    void SizeToSmall(bool small)
    {
        //if it gets true from the () then makes mouse small
        var sequence = DOTween.Sequence();
        sequence.Join(transform.DOScale(small ? smallSize : bigSize, 0.3f));
        sequence.Join(cameraTransform.DOScale(small
            ? new Vector3(0, cameraYOffsetSmall, 0)
            : new Vector3(0, cameraYOffsetBig, 0), 0.3f));
        // transform.localScale = (small) ? smallSize : bigSize;
        // characterController.height = (small) ? 0.5f : 1.2f;

        // var cameraHeightOffset = (small) ? new Vector3(0, cameraYOffsetSmall, 0) : new Vector3(0, cameraYOffsetBig, 0);
        // cameraTransform.localScale = cameraHeightOffset;
        isSmall = small;

        // In case we need to change the center of the character, we can uncomment it here
        // var characterControllerCenter = (small) ? new Vector3(0, characterController.height / 2, 0) : new Vector3(0, 0.6f, 0);
        // characterController.center = characterControllerCenter;
    }

    private void OnMushroomTaken()
    {
        SizeToSmall(false);
    }
    
    private void OnBoxInRightPlace()
    {
        SizeToSmall(true);
    }

    private void OnPlayerOutOfZone()
    {
        characterController.enabled = false;
        transform.position = LevelManager.Instance.PlayerStartPosition;
        characterController.enabled = true;
    }
    
    public bool IsSmall()
    {
        return isSmall;
    }

    // public void SetLookEnabled(bool enabled)
    // {
    //     canLook = enabled; // add this bool
    // }

}
