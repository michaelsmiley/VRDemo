using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public int healthPoints = 1;
    Animator animator;
    NavMeshAgent navMeshAgent;
    AudioManager audioManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (healthPoints <= 0)
        {
            DeathRoutine();
        }
    }

    public void TakeDamage(int damage)
    {
        audioManager.Play("EnemyTakeDamage");
        healthPoints -= damage;
        animator.SetTrigger("isDamaged"); //Anim section
    }

    private void DeathRoutine()
    {
        audioManager.Play("EnemyDeath");
        navMeshAgent.enabled = false;
        //Start death animation
        animator.SetBool("isDead", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);

        //Once animation ends, wait a couple of seconds, then destroy game object
        Destroy(gameObject, 5f);
    }
}
