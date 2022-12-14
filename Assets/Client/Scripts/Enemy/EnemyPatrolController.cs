using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : MonoBehaviour
{
    public List<Transform> points;

    //The int value for next point index
    public int nextID = 1;

    //Speed of movement or flying
    public float speed = 2;
    [SerializeField] public int damage;
    [SerializeField] private Health enemyHealth;
    private RaycastHit2D hit;

    private RaycastHit2D hitPlayer;

    //The value of that applies to ID for changing
    private int idChangeValue = 1;

    // private void Reset()
    // {
    //     Init();
    // }

    private void FixedUpdate()
    {
        MoveToNextPoint();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            //Check if we are at the start of the line (make the change +1)
            if (nextID == 0)
                idChangeValue = 1;
            //Apply the change on the nextID
            nextID += idChangeValue;
            Debug.Log("Meet with Player");
            var controller = collision.gameObject.GetComponentInParent<PlayerController>();
            controller.PlayerDmg(damage);
        }
    }

    // private void Init()
    // {
    //     //Make box collider trigger
    //     GetComponent<BoxCollider2D>().isTrigger = true;
    //
    //     //Create Root object
    //     var root = new GameObject(name + "_Root");
    //     //Reset Position of Root to enemy object
    //     root.transform.position = transform.position;
    //     //Set enemy object as child of root
    //     transform.SetParent(root.transform);
    //     //Create Waypoints object
    //     var waypoints = new GameObject("Waypoints");
    //     //Reset waypoints position to root        
    //     //Make waypoints object child of root
    //     waypoints.transform.SetParent(root.transform);
    //     waypoints.transform.position = root.transform.position;
    //     //Create two points (gameobject) and reset their position to waypoints objects
    //     //Make the points children of waypoint object
    //     var p1 = new GameObject("Point1");
    //     p1.transform.SetParent(waypoints.transform);
    //     p1.transform.position = root.transform.position;
    //     var p2 = new GameObject("Point2");
    //     p2.transform.SetParent(waypoints.transform);
    //     p2.transform.position = root.transform.position;
    //
    //     //Init points list then add the points to it
    //     points = new List<Transform>();
    //     points.Add(p1.transform);
    //     points.Add(p2.transform);
    // }

    private void MoveToNextPoint()
    {
        //Get the next Point transform
        var goalPoint = points[nextID];

        if (goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
        //Move the enemy towards the goal point

        var tempPos = goalPoint;
        transform.position = Vector2.MoveTowards(transform.position, tempPos.position, speed * Time.fixedDeltaTime);


        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            if (nextID == points.Count - 1)
                idChangeValue = -1;

            if (nextID == 0)
                idChangeValue = 1;

            nextID += idChangeValue;
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth.DmgUnit(damage);
        if (enemyHealth.GetHealth() <= 0)
        {
            Debug.Log("Enemy death");
            Destroy(gameObject);
        }
    }
}