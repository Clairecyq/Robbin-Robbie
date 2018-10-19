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
    public GameObject tutorialText1;
    public GameObject tutorialText2;
    public RectTransform.Axis anchor;

    public AudioClip robbieVictorySound1;
    public AudioClip robbieVictorySound2;
    public AudioClip robbieVictorySound3;
    public AudioClip robbieGameOverSound1;

    public GameObject energyBarOne;
    public bool gameOver = false;
    public bool levelFinish = false;

    private int snapshot = 0;

	void Awake () {
        LoggingManager.instance.RecordLevelStart(LoggingManager.instance.LevelID, LoggingManager.instance.LevelDescription);
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
                LoggingManager.instance.RecordLevelEnd();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
	}

    void FixedUpdate() {
        snapshot += 1;

        if (snapshot % 300 == 0) {
            int level = LoggingManager.instance.LevelID;
            float stamina = (float)robbie.GetComponent<CharacterController2D>().currentHidingPower / robbie.GetComponent<CharacterController2D>().getMaxHidingEnergy();
            float xpos = robbie.GetComponent<CharacterController2D>().transform.position.x;
            float ypos = robbie.GetComponent<CharacterController2D>().transform.position.x;
            LoggingManager.instance.RecordEvent(8, 
            "Snapshot - level: " + level.ToString() + "  stamina: " + stamina.ToString() + "  Xpos: " + xpos.ToString() + "  Ypos: " + ypos.ToString()
            ); 
            snapshot = 0;
        }
    }

    public void PauseOrResume() {
        if (isPaused) {
            Resume();
        } else {
            LoggingManager.instance.RecordEvent(9, "Pause");
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
        LoggingManager.instance.RecordEvent(7, "Level Reset");
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
            LoggingManager.instance.RecordEvent(1, "Robbie Died");
            gameOverText.SetActive(true);
            gameOver = true;
            SoundManager.instance.PlaySingle(robbieGameOverSound1);
            levelFinish = true;
        }
    }

    public void PickedDonut() {
        if (!gameOver) {
            LoggingManager.instance.RecordEvent(0, "Robbie Victory");
            finishLevelText.SetActive(true);
            finishLevelText2.SetActive(true);
            tutorialText1.SetActive(false);
            tutorialText2.SetActive(false);
            SoundManager.instance.RandomizeSfx(robbieVictorySound1, robbieVictorySound2, robbieVictorySound3);
            levelFinish = true;
        }
    }
}
