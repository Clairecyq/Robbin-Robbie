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
    public GameObject trashcan;
    public GameObject boot;
    public GameObject scoreText;
    public GameObject collectText;

    public GameObject targetTimeText;
    public GameObject energyBarOne;

    public RectTransform.Axis anchor;

    public AudioClip robbieVictorySound1;
    public AudioClip robbieVictorySound2;
    public AudioClip robbieVictorySound3;
    public AudioClip robbieGameOverSound1;

    RobbieMovement robbieMovement;

    public bool gameOver = false;
    public bool levelFinish = false;
    
    private int snapshot = 0;
    private int robbieScore = 0;

    public int levelId;
    public int totalNumLevels = 12; // this should not
    public int targetTime = 30; 
    private float timer = 0;
    private int minutesElapsed;


    public string levelDescription;

    void Awake ()
    {       
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        anchor = UnityEngine.RectTransform.Axis.Horizontal;
        if (LoggingManager.instance != null) LoggingManager.instance.RecordLevelStart(levelId, levelDescription);
        robbieMovement = robbie.GetComponent<RobbieMovement>();

        if (LoggingManager.instance.playerABValue == 3) {
            GameObject[] fires = GameObject.FindGameObjectsWithTag("Collectable");
            for (int idx = 0; idx < fires.Length; idx++) {
                GameObject fire = fires[idx];
                fire.SetActive(false);
            }
        }

        /*
         * TODO: create testing instance 
        if (GameStateManager.instance == null || !GameStateManager.instance.isInitialized)
        {
            Debug.Log("I am intializing");
            totalNumLevels = 12;
            GameStateManager.instance.Initialize(totalNumLevels);
            GameStateManager.instance.isInitialized = true;
        }
        */
	}

    public void ChangeToScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }

    public void ChangeToHome()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("r")) {
            Restart();
        }
        CharacterController2D char_component = robbie.GetComponent<CharacterController2D>();
        energyBarOne.GetComponent<Image>().fillAmount = Mathf.Min(1.0f, (float) char_component.currentHidingPower / char_component.getMaxHidingEnergy());

        //scoreText.GetComponent<Text>().text = "Score: " + robbieScore.ToString();

        //TODO: this needs to be modified
        if (levelFinish && !gameOver) {            

            robbieMovement.canMove = false;

            if (Input.GetKeyDown("c"))
            {
                if (LoggingManager.instance != null) {
                    LoggingManager.instance.RecordLevelEnd();
                }

                int currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;
                int levelStartingIndex = SceneManager.GetSceneByName("T1").buildIndex;

                if (currentLevelBuildIndex + 1 < GameStateManager.instance.totalNumLevels)
                {
                    GameStateManager.instance.levelsUnlocked[currentLevelBuildIndex] = true;
                    SceneManager.LoadScene(currentLevelBuildIndex + 1);
                }
            }
        }
	}

    void FixedUpdate() {
        snapshot += 1;
        timer += Time.deltaTime;

        minutesElapsed = (int) timer/60;
        string extraZero = "";
        if ((int) timer % 60 < 10) {
            extraZero = "0";
        }

        scoreText.GetComponent<Text>().text = minutesElapsed.ToString() + " : " + extraZero + (((int) timer) % 60).ToString();

        if (snapshot % 300 == 0) {
            packageInfo(18, "Snapshot - level:");
            snapshot = 0;
        }
    }

    public void PauseOrResume() {
        if (isPaused) {
            Resume();
        } else {
            packageInfo(19, "Pause");
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
        packageInfo(17, "Level Reset");
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
            robbieMovement.canMove = false;
            packageInfo(11, "Robbie Died - Generic");
            gameOverText.SetActive(true);
            gameOver = true;
            if (trashcan != null) trashcan.SetActive(false);
            if (boot != null) boot.SetActive(false);
            SoundManager.instance.PlaySingle(robbieGameOverSound1);
            levelFinish = true;
        }
    }

    public void displayVictoryStats() {
        // step 1: display all fires collected
        Camera cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        GameObject[] fires = GameObject.FindGameObjectsWithTag("Collectable");

        int ctr = 0;
        float xpos = robbie.GetComponent<CharacterController2D>().transform.position.x;
        float ypos = robbie.GetComponent<CharacterController2D>().transform.position.y;
        Vector2 fpos = new Vector2(xpos-fires.Length+1, ypos+2);

        for (int idx = 0; idx < fires.Length; idx++) {
            GameObject fire = fires[idx];
            if (fire.activeSelf) {
                if (fire.GetComponent<SpriteRenderer>().enabled) {
                    fire.GetComponent<SpriteRenderer>().color = Color.grey;
                }
                else {
                    ctr += 1;
                }
                fire.GetComponent<SpriteRenderer>().enabled = true;
                Vector2 fviewpos = fpos;
                fviewpos.x += (idx);
                fire.transform.position = fviewpos;
            }
        }
        if (ctr == fires.Length) {
            collectText.GetComponent<Text>().text = "All Fire Collected!";
        }
        else {
            collectText.GetComponent<Text>().text = "Fires Collected: " + ctr.ToString();
        }
        //Vector2 ctextLoc = fpos + new Vector2(-20,5);
        //collectText.transform.position = ctextLoc;
        collectText.SetActive(true);

        // step 2: display time

        string finishTime = scoreText.GetComponent<Text>().text;
        string finText = finishTime + "!  ";
        int lossTime = targetTime - (int) timer;
        int winTime = (int) timer - targetTime;
        if ((int) timer <= targetTime) {
            targetTimeText.GetComponent<Text>().text = finText + "-" + lossTime.ToString() + " fast!";
        }
        else {
            targetTimeText.GetComponent<Text>().text = finText + "+" + winTime.ToString() + " slow :(";
        }
        //targetTimeText.transform.position = fpos + new Vector2(-20,8);
        targetTimeText.SetActive(true);
        scoreText.SetActive(false);
    }

    public void PickedDonut() {
        if (!gameOver) {
            packageInfo(10, "Robbie Victory");
            finishLevelText.SetActive(true);
            finishLevelText2.SetActive(true);
            displayVictoryStats();
            if (tutorialText1!=null) tutorialText1.SetActive(false);
            if (tutorialText2!=null) tutorialText2.SetActive(false);
            SoundManager.instance.RandomizeSfx(robbieVictorySound1, robbieVictorySound2, robbieVictorySound3);
            levelFinish = true;
        }
    }

    public void obtainCoin() {
        robbieScore += 1;
    }

    public void packageInfo(int actionID, string action) {
        int level = levelId;
        float stamina = (float)robbie.GetComponent<CharacterController2D>().currentHidingPower / robbie.GetComponent<CharacterController2D>().getMaxHidingEnergy();
        float xpos = robbie.GetComponent<CharacterController2D>().transform.position.x;
        float ypos = robbie.GetComponent<CharacterController2D>().transform.position.y;
        int totalTime = (int) timer;
        if (LoggingManager.instance != null ) LoggingManager.instance.RecordEvent(
            actionID, 
            action + " " + level.ToString() + "  stamina: " + stamina.ToString() + "  Xpos: " + xpos.ToString() + "  Ypos: " + ypos.ToString() + "  playtime: " + totalTime.ToString() + " fires: " + robbieScore.ToString()
        );
    }
}
