using UnityEngine;

public class MainOptionsButtonClick : MonoBehaviour
{

    public void OnButtonSelect()
    {
        GetComponent<Animation>().Play("Options_button_select");

    }
    public void OnButtonDeselect()
    {
        GetComponent<Animation>().Play("Options_button_deselect");
    }
}
