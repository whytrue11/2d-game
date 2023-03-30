using UnityEngine;

public class BackButtonClick : MonoBehaviour
{
 
    public void OnButtonSelect()
    {
        GetComponent<Animation>().Play("Back_button_select");      
    }
    public void OnButtonDeselect()
    {
        GetComponent<Animation>().Play("Back_button_deselect");
    }
}
