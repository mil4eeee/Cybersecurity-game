using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Look")]
    public Transform cameraTransform;
    public float lookSensitivity = 0.08f;
    public float lookSmooth = 20f;
    public float pitchMin = -80f;
    public float pitchMax = 80f;

    private CharacterController controller;
    private InputManager input;

    private float yVelocity;
    private float pitch;

    private Vector2 currentLook;
    private Vector2 targetLook;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<InputManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        if (input == null)
            Debug.LogError("PlayerMovement: InputManager not found.");

        if (cameraTransform == null)
            Debug.LogError("PlayerMovement: Camera not assigned.");
    }

    private void Update()
    {
        if (MonitorScript.IsComputerOpen)
            return;

        HandleMovement();
        HandleLook();
    }

    private void HandleMovement()
    {
        if (input == null) return;

        Vector2 moveInput = input.MoveInput;
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded)
        {
            if (yVelocity < 0f)
                yVelocity = -2f;

            if (input.JumpPressedThisFrame)
            {
                yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        yVelocity += gravity * Time.deltaTime;
        controller.Move(Vector3.up * yVelocity * Time.deltaTime);
    }

    private void HandleLook()
    {
        if (input == null || cameraTransform == null) return;

        targetLook = input.LookInput * lookSensitivity;

        currentLook = Vector2.Lerp(
            currentLook,
            targetLook,
            1f - Mathf.Exp(-lookSmooth * Time.deltaTime)
        );

        transform.Rotate(Vector3.up * currentLook.x);

        pitch -= currentLook.y;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}