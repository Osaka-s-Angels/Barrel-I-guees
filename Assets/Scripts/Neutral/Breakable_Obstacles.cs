using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable_Obstacles : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

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
