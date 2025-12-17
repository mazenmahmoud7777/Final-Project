using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class EndlessRunnerController : MonoBehaviour
{
    [Header("Lane Movement")]
    public float laneDistance = 2f;      // lanes: -2, 0, +2
    public float laneChangeSpeed = 12f;  // slide speed

    [Header("Forward Speed")]
    public float walkSpeed = 6f;
    public float runSpeed = 10f;

    [Header("Jump")]
    public float jumpHeight = 1.6f;
    public float gravity = -20f;

    private CharacterController controller;

    private int laneIndex = 0;     // -1 left, 0 middle, 1 right
    private float verticalVel = 0f;

    private bool sprintHeld = false;
    private Vector2 moveInput;     // from Move action (we only use x)

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Forward movement
        float speed = sprintHeld ? runSpeed : walkSpeed;

        // Lane target
        float targetX = laneIndex * laneDistance;
        float newX = Mathf.Lerp(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);

        // Gravity
        if (controller.isGrounded && verticalVel < 0f)
            verticalVel = -2f;

        verticalVel += gravity * Time.deltaTime;

        // Build motion
        Vector3 motion = Vector3.zero;
    motion.z = speed;

        // Move sideways toward lane
        motion.x = (newX - transform.position.x) / Time.deltaTime;

        // Apply vertical
        motion.y = verticalVel;

        controller.Move(motion * Time.deltaTime);
    }

    // Input: Move (Vector2)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        // We only care about left/right for lanes
        if (moveInput.x > 0.5f)
            laneIndex = Mathf.Min(laneIndex + 1, 1);
        else if (moveInput.x < -0.5f)
            laneIndex = Mathf.Max(laneIndex - 1, -1);
    }

    // Input: Jump (Button)
    public void OnJump(InputValue value)
    {
        if (!value.isPressed) return;

        if (controller.isGrounded)
        {
            verticalVel = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Input: Sprint (Button, Press & Release)
    public void OnSprint(InputValue value)
    {
        sprintHeld = value.isPressed;
    }

    // Expose to animation script
    public bool SprintHeld => sprintHeld;
}
