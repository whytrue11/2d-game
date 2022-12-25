using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;


public class Attack : MonoBehaviour
{
    [SerializeField] private int damage;
    public float attackTime;
    public float startTimeAttack;

    public Transform attackLocation;
    public Vector2 attackRange;
    public LayerMask enemies;

    private void Start()
    {
         
    }

    void Update()
    {
        if( attackTime <= 0 )
        {
            if( Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("Attack");    
                Collider2D[] coll = Physics2D.OverlapBoxAll( attackLocation.position, attackRange, enemies );

                for (int i = 0; i < coll.Length; i++)
                {
                    if (coll[i].gameObject.tag.Contains("Enemy"))
                    {
                        EnemyController enemyController = null;
                        switch (coll[i].gameObject.tag)
                        {
                            case "EnemyPatrol":
                                enemyController = coll[i].gameObject.GetComponent<EnemyPatrolController>();
                                break;
                            case "EnemyPathFinder":
                                enemyController = coll[i].gameObject.GetComponent<EnemyPathFinderController>();
                                break;
                            case "EnemyPatrolZone":
                                enemyController = coll[i].gameObject.GetComponent<EnemyPatrolZoneController>();
                                break;
                        }
                        
                        //EnemyPatrolController enemyController = coll[i].gameObject.GetComponent<EnemyPatrolController>();
                        Debug.Log("Player dmg Enemy on " + damage);
                        Debug.Log("Attack location" + attackLocation.position);
                        Debug.Log("Attack range" + attackRange);
                        
                        enemyController.TakeDamage(damage);
                    }

                }
            }
            attackTime = startTimeAttack;
        }   else
        {
            attackTime -= Time.deltaTime;
                
        }
    }
        
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackLocation.position, attackRange);
    }
        
        
}
