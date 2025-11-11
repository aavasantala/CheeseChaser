using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class MouseController : MonoBehaviour
{
    //idk about these settings yet, need to tweak
    [Header("Settings for movement")]
    public float smallmouseSpeed = 6f;
    public float bigmouseSpeed = 3f;
    public float smallmouseJumpHeight = 1f;
    public float gravity = -9.81f;
    //idk what this does ^^

    //size tings
    [Header("Settings for mouse size")]
    public Vector3 smallSize = new Vector3(0.25f, 0.25f, 0.25f);
    public Vector3 bigSize = new Vector3(0.40f, 0.80f, 0.40f);

    [Header("Mouse Look Settings")]
    public Transform cameraTransform;
    public float mouseSensitivity = 150f;
    public float cameraYOffsetSmall = 0.25f;
    public float cameraYOffsetBig = 0.6f;


    [Header("Other")]
    
    //same with this idk what this does ^^
    private float xRotation = 0f;

    //some stuff, isgrounded makes it so that you can't jump mid air etc. velocity is for movement
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSmall = true;
    private Camera mainCam;
    private bool canLook = true;

    public UIManager uiManager;
    

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCam= GetComponent<Camera>();
        SizeToSmall(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CameraHandler();
        MovementHandler();
        SizeChangerCheck();
        
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
        float speed = isSmall ? smallmouseSpeed : bigmouseSpeed;

        characterController.Move(move * speed * Time.deltaTime);

        //jumping
        if (isSmall && Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(smallmouseJumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity*Time.deltaTime);
    }

    void SizeChangerCheck()
    {
        //here stuff for mushroom, now just a placeholder
        if (Input.GetKeyDown(KeyCode.F))
        {
            isSmall = !isSmall;
            SizeToSmall(isSmall);
        }
    }


    //This is the method that changes the size of the mouse
    //Usable with SizeToSmall(true) -> that then makes the mouse small, SizeToSmall(false) for big mouse.
    void SizeToSmall(bool small)
    {
        //if it gets true from the () then makes mouse small
        if (small)
        {
            transform.localScale = smallSize;
            characterController.height = 0.5f;
            characterController.center = new Vector3(0, characterController.height/2, 0);
            cameraTransform.localPosition = new Vector3(0, cameraYOffsetSmall, 0);
        }
        //otherwise make the mouse big
        else
        {
            transform.localScale = bigSize;
            characterController.height = 1.2f;
            characterController.center = new Vector3(0, 0.6f, 0);
            cameraTransform.localPosition = new Vector3(0, characterController.height / 2, 0);
        }
    }
    public interface IInteractable
    {
        void OnInteract();
    }

    public bool IsSmall()
    {
        return isSmall;
    }

    public void SetLookEnabled(bool enabled)
    {
        canLook = enabled; // add this bool
    }
}
