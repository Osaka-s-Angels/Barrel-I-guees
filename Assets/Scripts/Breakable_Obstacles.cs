using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Obstacles : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 3;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator=GetComponent<Animator>();
        
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        string phase = "Phase" + (maxHealth - currentHealth);
        if (currentHealth > 0)
        {
            animator.SetBool(phase, true);
        }
        else if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
            TakeDamage(1);
    
    }
}
