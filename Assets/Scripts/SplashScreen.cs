using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

    LevelController levelController;

	// Use this for initialization
	void Start () {

        levelController = FindObjectOfType<LevelController>();

        Invoke("LoadFirstScene", 2f);
	}

    void LoadFirstScene()
    {
        levelController.NextLevel();
    }
}
