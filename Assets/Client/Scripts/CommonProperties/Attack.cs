using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackCooldown;

    [SerializeField] private Transform attackLocation;
    [SerializeField] private Vector2 attackRange;
    [SerializeField] private LayerMask enemies;

    private bool canAttack = true;

    public IEnumerator AttackEnemy()
    {
        canAttack = false;
        animator.SetTrigger("Attack");
        Debug.Log("Attack");    
        Collider2D[] coll = Physics2D.OverlapBoxAll( attackLocation.position, attackRange, enemies);
        EnemyController enemyController = null;
        for (int i = 0; i < coll.Length; i++)
        {
            if (coll[i].gameObject.tag.Contains("Enemy") || coll[i].gameObject.tag.Contains("Boss"))
            {
                
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
                    case "Boss":
                        enemyController = coll[i].gameObject.GetComponent<BossController>();
                        break;
                }
                        
                //EnemyPatrolController enemyController = coll[i].gameObject.GetComponent<EnemyPatrolController>();
                Debug.Log("Player dmg Enemy on " + damage);
                Debug.Log("Attack location" + attackLocation.position);
                Debug.Log("Attack range" + attackRange);   
            }
        }
        if(enemyController != null)
        {
            enemyController.TakeDamage(damage);
        }
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public bool GetCanAttack()
    {
        return this.canAttack;
    }

      
    public void SetAttackCooldown(float cooldown)
    {
        this.attackCooldown = cooldown;
        DataHolder.playerWeaponAttackCooldown = cooldown;
    }
    public float GetAttackCooldown()
    {
        return attackCooldown;
    }
    public void SetWeaponAnimation(int animationLayerPos)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animationLayerPos, 1);

        DataHolder.playerWeaponAnimation = animationLayerPos;
    }
    public int GetWeaponAnimation()
    {
        return DataHolder.playerWeaponAnimation;
    }
    public int GetDamage()
    {
        return damage;
    }
    public void SetDamage(int weaponDamage)
    {
        damage = weaponDamage;
        DataHolder.playerDamage = damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackLocation.position, attackRange);
    }
      
}
