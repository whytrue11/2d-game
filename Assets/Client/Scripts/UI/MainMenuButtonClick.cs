using UnityEngine;

public class MainMenuButtonClick : MonoBehaviour
{

    public void OnButtonSelect()
    {
        GetComponent<Animation>().Play("Menu_button_select");
    }
    public void OnButtonDeselect()
    {
        GetComponent<Animation>().Play("Menu_button_deselect");
    }
}
