using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] string levelName = "SampleScene";
    [SerializeField] int hitsToTake = 10;
    [SerializeField] float respawnTime = 3f;
    [SerializeField] Text hpText;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject hitScreen;
    AudioManager audioManager;
    LevelController levelController;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        levelController = FindObjectOfType<LevelController>(); //
        deathScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            print ("Enemy touched the Player");
            audioManager.Play("PlayerHurt");
            StartCoroutine(PlayerHit());
            hitsToTake -= 1;
        }
    }

    private void Update()
    {
        if (hitsToTake <= 0)
        {
            print("Player Has Died");
            StartCoroutine(PlayerDeath());
            return;
        }

        hpText.text = hitsToTake.ToString();
    }

    //
    IEnumerator PlayerDeath()
    {
        audioManager.Play("PlayerDeath");
        deathScreen.SetActive(true);
        gun.SetActive(false);
        yield return new WaitForSeconds(respawnTime);
        //SceneManager.LoadScene(levelName);
        levelController.RestartGame();
    }

    IEnumerator PlayerHit()
    {
        hitScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitScreen.SetActive(false);
    }
}
