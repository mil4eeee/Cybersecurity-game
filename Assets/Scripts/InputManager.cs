using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput controls;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool InteractPressedThisFrame { get; private set; }
    public bool JumpPressedThisFrame { get; private set; }

    private void Awake()
    {
        controls = new PlayerInput();
    }

    private void OnEnable()
    {
        controls.Player.Enable();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;

        controls.Player.Look.performed += OnLookPerformed;
        controls.Player.Look.canceled += OnLookCanceled;

        controls.Player.Interact.performed += OnInteractPerformed;
        controls.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMovePerformed;
        controls.Player.Move.canceled -= OnMoveCanceled;

        controls.Player.Look.performed -= OnLookPerformed;
        controls.Player.Look.canceled -= OnLookCanceled;

        controls.Player.Interact.performed -= OnInteractPerformed;
        controls.Player.Jump.performed -= OnJumpPerformed;

        controls.Player.Disable();
    }

    private void Update()
{
    InteractPressedThisFrame = false;
    JumpPressedThisFrame = false;
}

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        MoveInput = Vector2.zero;
    }

    private void OnLookPerformed(InputAction.CallbackContext ctx)
    {
        LookInput = ctx.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext ctx)
    {
        LookInput = Vector2.zero;
    }

    private void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        InteractPressedThisFrame = true;
        Debug.Log("E pressed");
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        JumpPressedThisFrame = true;
    }
    
}