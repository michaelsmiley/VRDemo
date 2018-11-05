using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    [SerializeField] Animator animator;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        StartCoroutine(FadeOut());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeOut()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
