using UnityEngine;

public class EnemyPatrolZoneController : EnemyController
{
    [SerializeField] private Transform rayCast;
    [SerializeField] private LayerMask raycastMask;
    [SerializeField] private float rayCastLength;
    [SerializeField] private float attackDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] public float timer { get; set; }
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;
    private bool attackMode;
    public bool cooling { get; set; }
    private float distance;
    private RaycastHit2D hit;
    public bool inRange { get; set; }
    private float intTimer;
    private Transform target;
    public bool atacked = false;
   
    private void Awake()
    {
        SelectTarget();
        intTimer = timer;
    }
    private void FixedUpdate()
    {
        if (!attackMode) Move();

        if (!InsideOfLimits() && !inRange)
            SelectTarget();

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, raycastMask);
            RaycastDebugger();
        }


        if (hit.collider != null)
            EnemyLogic();
        else if (hit.collider == null) inRange = false;

        if (inRange == false) StopAttack();
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Player"))
        {
            target = trig.transform;
            inRange = true;
            Flip();
        }
    }

    private void EnemyLogic()
    {
        if (cooling)
        {
            Cooldown();
            return;
        }

        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
            StopAttack();
        else if (attackDistance >= distance && cooling == false) Attack();
    }

    private void Move()
    {
        var targetPosition = new Vector2(target.position.x, transform.position.y);

        transform.position =
            Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        timer = intTimer;
        atacked = true;
        attackMode = true;
        Debug.Log(cooling);
        Debug.Log("Meet with Player");
        var controller = target.gameObject.GetComponentInParent<PlayerController>();
        animator.SetTrigger("Attack");
        controller.PlayerDmg(damage);

        cooling = true;
    }

    private void Cooldown()
    {
        timer -= Time.fixedDeltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    private void StopAttack()
    {
        cooling = false;
        attackMode = false;
    }

    private void RaycastDebugger()
    {
        if (distance > attackDistance)
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        else if (attackDistance > distance)
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
    }

    public bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        var distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        var distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
            target = leftLimit;
        else
            target = rightLimit;

        Flip();
    }

    private void Flip()
    {
        var rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            Debug.Log("Twist");
            atacked = true;
            rotation.y = 0;
        }
        
        transform.eulerAngles = rotation;
    }

    public override void TakeDamage(int damage)
    {
        enemyHealth.DmgUnit(damage);
        if (enemyHealth.GetHealth() <= 0)
        {
            Debug.Log("Enemy death");
            FindObjectOfType<GameManager>().AddCoins(2);
            Destroy(gameObject);
        }
    }
}
