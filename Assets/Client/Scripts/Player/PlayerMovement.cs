using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float runSpeed;
    [SerializeField] private Animator animator;

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
    }

    void Update()
    {
        if (gameManager.pause)
        {
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
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
        jump = false;
        dash = false;
    }
}

