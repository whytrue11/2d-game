using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float runSpeed;

    private PlayerController controller;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;


    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
