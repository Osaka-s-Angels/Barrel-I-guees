using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeEnemy : MonoBehaviour
{
    Animator anim;

    [Header("Information about GameObjects")]
    [SerializeField] Transform sword;
    [SerializeField] CircleCollider2D attackRange;
    [SerializeField] CircleCollider2D followRange;
    private Transform target;

    [Header("Mathematical Variables")]
    [SerializeField] float speed = 3f;
    [SerializeField] Transform firePoint;
    [SerializeField] float attackRadius = 0.5f;
    [SerializeField] LayerMask enemyLayers;
    private float nextAttack = 0.15f;
    [SerializeField] private float attackRate = 0.5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) <= attackRange.radius && Time.time > nextAttack)
            {
                Attack();
                nextAttack = Time.time + attackRate;
            }
            else if (Vector2.Distance(transform.position, target.position) <= (followRange.radius) && Vector2.Distance(transform.position, target.position) > (attackRange.radius))
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }

        if (target == null)
        {
            transform.position = this.transform.position;
        }
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 lookDir = target.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            sword.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision )
    {
        if (collision.gameObject.tag == "Player" && collision == followRange)
        {
            target = null;
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position,attackRadius,enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(firePoint.position, attackRadius);
    }
}
