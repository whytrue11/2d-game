using UnityEngine;

public class FolowPlayer : MonoBehaviour
{
    public GameObject player;
  
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
}
