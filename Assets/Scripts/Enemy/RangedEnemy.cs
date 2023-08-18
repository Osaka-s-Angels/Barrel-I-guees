using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Information about GameObjects")]
    [SerializeField] Transform bow;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] CircleCollider2D attackRange;
    [SerializeField] CircleCollider2D followRange;
    private Transform target;

    [Header("Mathematical Variables")]
    [SerializeField] float speed = 3f;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletForce = 20f;
    private float nextShot = 0.15f;
    [SerializeField] private float fireRate = 0.5f;

    private void Update()
    {
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.position) <= attackRange.radius && Time.time > nextShot)
            {
                Shoot();
                nextShot = Time.time + fireRate;
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
        if(target != null)
        {
            Vector2 lookDir = target.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            bow.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target = collision.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision == followRange)
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        
    }
}
