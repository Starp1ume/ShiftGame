using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private PhaseManager phaseManager;

    private Rigidbody2D playerBody;
    private Collider2D playerCollider;


    [SerializeField] private float moveSpeed = 5f;
    private float moveInput;

    [SerializeField] private float jumpSpeed = 8f;
    private bool jumpRequested;
    private bool shiftRequested;

    private void Awake()
    {
        if (groundLayer == default)
        {
            Debug.LogError("Ground 层级错误！", this);
        }
        if (phaseManager == null)
        {
            Debug.LogError("phaseManager 未绑定 世界管理器！", this);
        }

        playerBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        moveInput = 0f;

        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpRequested = true;
        }
        
        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            shiftRequested = true;
        }

        if (Keyboard.current.aKey.isPressed
            || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput -= 1f;
        }

        if (Keyboard.current.dKey.isPressed
            || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput += 1f;
        }
    }

    private void FixedUpdate()
    {
        playerBody.linearVelocityX = moveInput * moveSpeed;

        if (jumpRequested)
        {
            jumpRequested = false;

            if (IsGrounded())
            {
                playerBody.linearVelocityY = jumpSpeed;
            }
        }

        if (shiftRequested)
        {
            shiftRequested = false;
            phaseManager.TrySwitchWorld(playerCollider);
        }
    }

    private bool IsGrounded()
    {
        Bounds bounds = playerCollider.bounds;

        Vector2 checkCenter = new Vector2(
            bounds.center.x,
            bounds.min.y - 0.03f
            );

        Vector2 checkSize = new Vector2(
            bounds.size.x * 0.9f,
            0.1f
            );

        return Physics2D.OverlapBox(
            checkCenter,
            checkSize,
            0f,
            groundLayer
            ) != null;
    }
}