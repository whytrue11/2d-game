using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : EnemyController
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

    public override void TakeDamage(int damage)
    {
        enemyHealth.DmgUnit(damage);
        if (enemyHealth.GetHealth() <= 0)
        {
            Debug.Log("Enemy death");
            Destroy(gameObject);
        }
    }
}