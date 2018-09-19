using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public GameObject finishLevelText;
    public GameObject gameOverText;
    public bool gameOver = false;
    public bool levelFinish = false;

	void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (levelFinish) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

    public void RobbieDied() {
        gameOverText.SetActive(true);
        gameOver = true;
    }

    public void PickedDonut() {
        finishLevelText.SetActive(true);
        //levelFinish = true;

    }
}
