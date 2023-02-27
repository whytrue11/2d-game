using UnityEngine;

public class ExitButtonClick : MonoBehaviour
{
 
    public void OnButtonSelect()
    {
        GetComponent<Animation>().Play("Exit_button_select");
    }
    public void OnButtonDeselect()
    {
        GetComponent<Animation>().Play("Exit_button_deselect");
    }
}
