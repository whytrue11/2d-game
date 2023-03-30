using UnityEngine;

public class PlayButtonClick : MonoBehaviour
{
 
    public void OnButtonSelect()
    {
        GetComponent<Animation>().Play("Play_button_select");
        
    }
    public void OnButtonDeselect()
    {
        GetComponent<Animation>().Play("Play_button_deselect");
    }
}
