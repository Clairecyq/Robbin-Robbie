using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public static bool isPaused;

    public GameObject robbie;
    public GameObject finishLevelText;
    public GameObject finishLevelText2;
    public GameObject gameOverText;
    public RectTransform.Axis anchor;

    public AudioClip robbieVictorySound1;
    public AudioClip robbieVictorySound2;
    public AudioClip robbieVictorySound3;
    public AudioClip robbieGameOverSound1;

    public GameObject energyBarOne;
    public bool gameOver = false;
    public bool levelFinish = false;

	void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        anchor = UnityEngine.RectTransform.Axis.Horizontal;
	}

    public void ChangeToScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("r")) {
            Restart();
        }
        CharacterController2D char_component = robbie.GetComponent<CharacterController2D>();
        energyBarOne.GetComponent<Image>().fillAmount = Mathf.Min(1.0f, (float) char_component.currentHidingPower / char_component.getMaxHidingEnergy());

        //TODO: this needs to be modified
        if (levelFinish && !gameOver) {
            if (Input.GetKeyDown("c"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
	}

    public void PauseOrResume() {
        if (isPaused) {
            Resume();
        } else {
            Pause();
        }
    }

    void Resume() {
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        levelFinish = false;
        gameOver = false;
        Vector3 energyScale = energyBarOne.transform.localScale;
        energyScale.x = 1.0f;
        energyBarOne.transform.localScale = energyScale;
    }

    public void RobbieDied() {
        if (!levelFinish)
        {
            gameOverText.SetActive(true);
            gameOver = true;
            SoundManager.instance.PlaySingle(robbieGameOverSound1);
            levelFinish = true;
        }
    }

    public void PickedDonut() {
        if (!gameOver) {
            finishLevelText.SetActive(true);
            finishLevelText2.SetActive(true);
            SoundManager.instance.RandomizeSfx(robbieVictorySound1, robbieVictorySound2, robbieVictorySound3);
            levelFinish = true;
        }
    }
}
