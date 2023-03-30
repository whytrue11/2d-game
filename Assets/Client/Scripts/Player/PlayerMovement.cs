using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private Joystick joystick;

    private GameManager gameManager;

    private PlayerController controller;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    private bool dash = false;

    public void SetRunSpeed(float speed)
    {
        runSpeed = speed;
    }
    public float GetRunSpeed()
    {
        return runSpeed;
    }

    void Start()
    {
        controller = GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("Utils").GetComponent<GameManager>();
        runSpeed = DataHolder.playerRunSpeed;
    }

    void Update()
    {
        if (gameManager.pause)
        {
            horizontalMove = 0;
            animator.SetFloat("HorizontalMove", 0);
            return;
        }

        if (joystick.Horizontal >= 0.2f)
        {
            horizontalMove = runSpeed;
        }
        else if (joystick.Horizontal <= -0.2f)
        {
            horizontalMove = -runSpeed;
        }
        else
        {
            horizontalMove = 0;
        }
        animator.SetFloat("HorizontalMove", Mathf.Abs(horizontalMove));

        float verticalMove = joystick.Vertical;

        if (verticalMove <= -0.5f)
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }
    }
    public void MakeDash()
    {
        dash = true;
    }
    public void MakeJump()
    {
        jump = true;
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
        jump = false;
        dash = false;
    }
}
