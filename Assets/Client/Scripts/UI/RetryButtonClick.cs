using UnityEngine;

public class RetryButtonClick : MonoBehaviour
{
 
    public void OnButtonSelect()
    {
        GetComponent<Animation>().Play("Retry_button_select");   
    }
    public void OnButtonDeselect()
    {
        GetComponent<Animation>().Play("Retry_button_deselect");
    }
}
