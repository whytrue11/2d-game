using UnityEngine;

public class ScoresButtonClick : MonoBehaviour
{
 
    public void OnButtonSelect()
    {
        GetComponent<Animation>().Play("Scores_button_select");   
    }
    public void OnButtonDeselect()
    {
        GetComponent<Animation>().Play("Scores_button_deselect");
    }
}
