using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour {

    public Button buttonComponent;
    public Text currentLevel;
    public Image locked;
    public Texture2D timeBadge;
    public Texture2D candyBadge;

    private LevelSelectList levelSelectList;
    private bool currentLevelPassed;

    void Start () {
		
	}
	
    public void Setup(bool currentLevelPassed, int currentLevel, LevelSelectList currentLevelSelectList)
    {
        this.currentLevel.text = currentLevel.ToString();
        this.currentLevelPassed = currentLevelPassed;
        levelSelectList = currentLevelSelectList;   
    }
}
