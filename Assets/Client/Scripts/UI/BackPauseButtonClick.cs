using UnityEngine;

public class BackPauseButtonClick : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void OnButtonSelect()
    {
        animator.SetTrigger("button_select");
    }
    public void OnButtonDeselect()
    {
        animator.SetTrigger("button_deselect");
    }
}
