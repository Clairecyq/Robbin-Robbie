using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance;

    public GameObject robbie;
    public GameObject finishLevelText;
    public GameObject finishLevelText2;
    public GameObject gameOverText;

    public GameObject energyBarOne;
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
            levelFinish = false;
            Vector3 energyScale = energyBarOne.transform.localScale;
            energyScale.x = 1.0f;
            energyBarOne.transform.localScale = energyScale;

        }

        // this code requires items from character controller to make changes to something in game controller
        // needs to be moved
        Vector3 energyscale = energyBarOne.transform.localScale;
        //Debug.Log(CharacterController2D.instance.currentHidingPower);
        CharacterController2D char_component = robbie.GetComponent<CharacterController2D>();
        energyscale.x = Mathf.Min(1.0f, (float) char_component.currentHidingPower / char_component.getMaxHidingEnergy());
        energyBarOne.transform.localScale = energyscale;
        // end problematic code

        //TODO: this needs to be modified
        if (levelFinish) {
            if (Input.GetKeyDown("c"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
	}

    public void RobbieDied() {
        if (!levelFinish)
        {
            gameOverText.SetActive(true);
            gameOver = true;
        }
    }

    public void PickedDonut() {
        if (!gameOver) {
            finishLevelText.SetActive(true);
            finishLevelText2.SetActive(true);
            levelFinish = true;
        }
    }
}
