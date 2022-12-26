using System;
using System.Linq;
using Pathfinding;
using UnityEngine;
using Random = System.Random;

public class BossController : EnemyController
{
    [Header("Pathfinding")] public Transform target;
    public float activateDistance = 100f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")] public float speed = 200f;
    public float nextWaypointDistance = 100f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")] public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    public float timer;
    private float intTimer;
    
    private bool cooling;

    // [SerializeField] private LayerMask whatIsGround; 
    // [SerializeField] private Transform groundCheck;     
    const float groundedRadius = .9f;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private int damage;

    public Transform bulletPos;
    public GameObject bulletPrefab;
    public float bulletForce = 2f;
    private bool isFacingRight = true;
    private int[] attacks;
    private int currentAttack;
    private int countOfAttacks = 10;
    public void Start()
    {
        int Min = 0;
        int Max = 2;
        Random randNum = new Random();
        currentAttack = 0;
        attacks = Enumerable
            .Repeat(0, countOfAttacks)
            .Select(i => randNum.Next(Min, Max))
            .ToArray();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
        attacks.ToList().ForEach(i => Debug.Log(i));
        
    }

    public void Awake()
    {
        intTimer = timer;
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

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything
        // The player is grounded if a circlecast to the groundCheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        // for (int i = 0; i < colliders.Length; i++)
        // {
        //     if (colliders[i].gameObject != gameObject)
        //     {
        //         isGrounded = true;
        //     }
        // }
        Vector3 startOffset = transform.position -
                              new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        Debug.Log(isGrounded);
        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * (speed * Time.deltaTime);

        // Jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * (speed * jumpModifier));
            }
        }

        // Movement
        rb.AddForce(force);

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
                isFacingRight = true;
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f)
            {
                isFacingRight = false;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z);
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
            Attack(trig);
        }
    }

    private void Attack(Collider2D trig)
    {
        if (cooling)
        {
            Cooldown();
            return;
        }

        timer = intTimer;
        Debug.Log("Meet with Player");
        if(attacks.Length <= currentAttack)
        {
            currentAttack = 0;
        }
        int attack = attacks[currentAttack];
        
        if (attack == 0)
        {
           // Close attack
             Debug.Log("CLOSE ATTACK");
             var controller = trig.gameObject.GetComponentInParent<PlayerController>();
             controller.PlayerDmg(damage); 
        }
        else
        {
            //distant attack
            Debug.Log("DISTANT ATTACK");
            Shoot();    
        }

        currentAttack++;
        
        
        cooling = true;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (isFacingRight)
        {
            rb.AddForce(bulletPos.right * bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(-bulletPos.right * bulletForce, ForceMode2D.Impulse);
        }
    }


    private void Cooldown()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0 && cooling)
        {
            cooling = false;
            timer = intTimer;
        }
    }
}