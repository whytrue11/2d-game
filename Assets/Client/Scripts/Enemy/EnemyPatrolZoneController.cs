using UnityEngine;

public class EnemyPatrolZoneController : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    private bool attackMode;
    private bool cooling;
    private float distance;


    private RaycastHit2D hit;
    private bool inRange;
    private float intTimer;
    private Transform target;


    private void Awake()
    {
        SelectTarget();
        intTimer = timer;
    }

    private void Update()
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
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
            StopAttack();
        else if (attackDistance >= distance && cooling == false) Attack();

        if (cooling) Cooldown();
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
        attackMode = true;
    }

    private void Cooldown()
    {
        timer -= Time.deltaTime;

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

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
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
            rotation.y = 0;
        }
        
        transform.eulerAngles = rotation;
    }
}