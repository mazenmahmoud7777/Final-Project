using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class RunnerAnimationDriver : MonoBehaviour
{
    private Animator anim;
    private EndlessRunnerController runner;

    void Awake()
    {
        anim = GetComponent<Animator>();
        runner = GetComponent<EndlessRunnerController>();
    }

    void Update()
    {
        // In an endless runner, you're always moving forward,
        // so walking is true unless sprint is held.
        bool isRunning = runner != null && runner.SprintHeld;
        bool isWalking = !isRunning; // simple logic (walk when not sprinting)

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isRunning", isRunning);
    }

    // Trigger Jump animation immediately when Space is pressed
    public void OnJump(InputValue value)
    {
        if (value.isPressed)
            anim.SetTrigger("Jump");
    }
}
