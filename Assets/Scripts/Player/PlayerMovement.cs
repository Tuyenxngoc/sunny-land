using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerController controller;

    public float runSpeed = 40.0f;
    public float climbSpeed = 20.0f;

    private Vector2 moveDirection = Vector2.zero;
    bool jumpKeyPress = false;
    bool crouchKeyPress = false;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void ProcessInput()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector2(horizontalInput * runSpeed, verticalInput * climbSpeed);

        if (Input.GetButtonDown("Jump"))
        {
            jumpKeyPress = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouchKeyPress = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouchKeyPress = false;
        }
    }

    private void MovePlayer()
    {
        controller.Move(moveDirection * Time.fixedDeltaTime, jumpKeyPress, crouchKeyPress);
        jumpKeyPress = false;
    }
}
