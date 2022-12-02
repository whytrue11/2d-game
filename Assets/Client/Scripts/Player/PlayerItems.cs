using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    private PlayerController controller;
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            controller.Action(); 
        }
    }
}
