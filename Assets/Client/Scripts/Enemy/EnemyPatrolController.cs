using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolController : EnemyController
{
    [SerializeField] private int damage;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private Animator animator;

    [SerializeField] private List<Transform> points;
    //The int value for next point index
    [SerializeField] private int nextID = 1;

    //Speed of movement or flying
    [SerializeField] private float speed = 2;

    //The value of that applies to ID for changing
    private int idChangeValue = 1;
    public bool attacked = false;
    public bool walked = false;
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
            animator.SetTrigger("Attack");
            attacked = true;
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
            walked = true;
        }
    }

    public override void TakeDamage(int damage)
    {
        enemyHealth.DmgUnit(damage);
        if (enemyHealth.GetHealth() <= 0)
        {
            Debug.Log("Enemy death");
            FindObjectOfType<GameManager>().AddCoins(1);
            Destroy(gameObject);
        }
    }

    public int getNextId()
    {
        return nextID;
    }

    public int getIdChangeValue()
    {
        return idChangeValue;
    }

    public List<Transform> getPoints()
    {
        return points;
    }

    public void setPoints(List<Transform> newPoints)
    {
        points = newPoints;
    }
}
