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

    /*void Update()
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
    }*/
    
    //Keyboard (temp)
    void Update()
    {
        if (gameManager.pause)
        {
            horizontalMove = 0;
            animator.SetFloat("HorizontalMove", 0);
            return;
        }
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("HorizontalMove", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        if (Input.GetButtonDown("Dash"))
        {
            dash = true;
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
