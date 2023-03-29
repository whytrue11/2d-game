using System;
using Pathfinding;
using UnityEngine;
using System.Collections;

public class EnemyPathFinderController : EnemyController
{
   [Header("Pathfinding")]
    [SerializeField] private float activateDistance = 100f;
    [SerializeField] private float pathUpdateSeconds = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;

    [Header("Physics")]
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDistance = 100f;
    [SerializeField] private float jumpNodeHeightRequirement = 0.8f;
    [SerializeField] private float jumpForce = 6.5f;


    [Header("Custom Behavior")]
    [SerializeField] private bool followEnabled = true;
    [SerializeField] private bool jumpEnabled = true;
    [SerializeField] private bool directionLookEnabled = true;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] Animator animator;

    private Transform target;
    private bool canAttack = true;
    private Path path;
    private int currentWaypoint = 0;
    private bool grounded;
    private Seeker seeker;
    private Rigidbody2D rb;
    public Boolean pathFound = false;
        
    private const float groundedRadius = .2f; 
   
    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    public override void TakeDamage(int damage)
    {
        enemyHealth.DmgUnit(damage);
        if (enemyHealth.GetHealth() <= 0)
        {
            Debug.Log("Enemy death");
            FindObjectOfType<GameManager>().AddCoins(3);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        pathFound = true;
        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            pathFound = false;
            return;
        }


        bool wasgrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasgrounded)
                {
                    animator.SetBool("Jump", false);
                }
            }
        }

       
      
        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * (speed * Time.deltaTime);

     // Jump
        if (jumpEnabled && grounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
             
                rb.velocity = Vector2.up * jumpForce;
                animator.SetBool("Jump", true);
            }
        }

        
        // Movement
        rb.AddForce(Vector2.right * force);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        Debug.Log("AAA");
        if (!p.error)
        {
            Debug.Log("BBBB");
            path = p;
            currentWaypoint = 0;
        }
    }
    private void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            if(canAttack)
            {
                StartCoroutine(Attack(trig));
            }
        }
    }
    private IEnumerator Attack(Collider2D trig)
    {
        canAttack = false;
        Debug.Log("Meet with Player");
        var controller = trig.gameObject.GetComponentInParent<PlayerController>();
        animator.SetTrigger("Attack");
        controller.PlayerDmg(damage);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    
}
