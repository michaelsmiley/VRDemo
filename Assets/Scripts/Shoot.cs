using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {

    //Variables for Gun
    [SerializeField] new Camera camera; //Reference the camera
    [SerializeField] int damage = 1; //Set damage to enemy
    [SerializeField] int range = 100; //Range of raycast
    [SerializeField] int clipSize = 7; //Set number of bullets per clip
    [SerializeField] float reloadTime = 2f; //Set reload time
    int currentAmmo; //Track current Ammo before reload
    bool isReloading = false; //check if reloading

    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] Animator animator;

    [SerializeField] Text bulletText;

    AudioManager audioManager;
    
    private void Start()
    {
        if (currentAmmo <= 0)
        {
            currentAmmo = clipSize;
        }

        camera = FindObjectOfType<Camera>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        //Check if reloading
        if (isReloading)
        {
            return;
        }

        //Check current ammo
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        //Check Input
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            FireWeapon();
        }

        bulletText.text = currentAmmo.ToString();
    }

    void FireWeapon()
    {
        audioManager.Play("GunShot");
        //Near the end add this
        currentAmmo--;

        //This comes late
        muzzleFlash.Play();

        //This is near the start
        RaycastHit hit;

        //This will check if the raycast hits anything
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            //Check if raycast works, send message to console
            print("You hit " + hit.transform.name);

            //-------- This is written after the enemy script is written

            //Reference scripts
            EnemyHealth enemy = hit.transform.GetComponent<EnemyHealth>();

            //Check if hit body
            if (enemy != null) //Check if object has enemy script attached
            {
                enemy.TakeDamage(damage); //make enemy take damage by referring to TakeDamage method
            }
        }
    }

    //Make a coroutine
    IEnumerator Reload()
    {
        audioManager.Play("GunReload");
        isReloading = true;
        print("OKAY I'M RELOADING!");

        animator.SetBool("Reloading", true); //

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false); //

        yield return new WaitForSeconds(0.25f); //

        currentAmmo = clipSize;
        isReloading = false;
    }
}
