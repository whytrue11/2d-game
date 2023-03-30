using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private PlayerController controller;
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("PickUpItem"))
        {
            controller.PickUpItem(); 
        }

        if (Input.GetButtonDown("Attack"))
        {
            controller.AttackEnemy();
        }
    }
}
