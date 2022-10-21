using System;
using UnityEditor;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float upLimit;
    [SerializeField] private float downLimit;
  
    void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(player.transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(player.transform.position.y, downLimit, upLimit),
            -10f);
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector2(leftLimit, upLimit),
            new Vector2(leftLimit, downLimit));
        Gizmos.DrawLine(
            new Vector2(rightLimit, upLimit),
            new Vector2(rightLimit, downLimit));
        Gizmos.DrawLine(
            new Vector2(leftLimit, upLimit),
            new Vector2(rightLimit, upLimit));
        Gizmos.DrawLine(
            new Vector2(leftLimit, downLimit),
            new Vector2(rightLimit, downLimit));
    }
}
