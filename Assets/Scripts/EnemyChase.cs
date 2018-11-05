using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour {

    [SerializeField] Transform theTarget; //Reference the position of the target
    [SerializeField] float forwardSpeed = 5f;
    [SerializeField] float distancefromTarget = 1f; //Can make private once the best value is found

    Animator animator; //
    EnemyHealth enemyHealth;
    AudioManager audioManager;

    //****This comes during spawn point section
    private void Awake()
    {
        if (theTarget == null)
        {
            theTarget = GameObject.FindGameObjectWithTag("Player").transform; //Set the target
        }
        
        animator = GetComponent<Animator>(); //
        enemyHealth = GetComponent<EnemyHealth>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    //****

    void Update () { //if else statements happen during animation section

        FaceTarget();

        if (enemyHealth.healthPoints > 0)
        {
            //Check distance 
            if (Vector3.Distance(theTarget.position, this.transform.position) < distancefromTarget)
            {
                AttackTarget();
            }
            else
            {
                ChaseTarget();
            }
        }
	}

    void ChaseTarget()
    {
        audioManager.Play("EnemyChase");
        //Move Towards the target
        this.transform.Translate(0, 0, 0.01f * forwardSpeed); //x, y, z axis

        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
    }

    void AttackTarget()
    {
        audioManager.Play("EnemyAttack");
        //Enter attack code here
        animator.SetBool("isAttacking", true);
        animator.SetBool("isWalking", false);
    }

    void FaceTarget()
    {
        //Find the direction of the target
        Vector3 targetDirection = theTarget.position - this.transform.position;

        //Makes sure the enemy remains in the upright position
        targetDirection.y = 0; //

        //Rotate smoothly to face the target
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(targetDirection), 0.1f);
    }
}
