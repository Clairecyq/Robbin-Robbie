using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;

public class LevelSelectList : MonoBehaviour {

    private GameObject levelSelectButton;
    private bool godMode = true;
    public Transform contentPanel;

    // Use this for initialization
    void Start () {
        RefreshDisplay();     
	}
    
    public void RefreshDisplay()
    {
        AddButtons();    
    }

    [SerializeField]
    private LevelSelector p_ls;

    private void AddButtons()
    {
        if (GameStateManager.instance != null)
        {
            for (int currentLevel = 0; currentLevel < GameStateManager.instance.totalNumLevels; currentLevel++)
            {
                LevelSelectButton newLevelSelectButton = (Instantiate(Resources.Load("LevelSelectButton")) as GameObject).GetComponent<LevelSelectButton>();
                newLevelSelectButton.transform.SetParent(contentPanel);

                bool currentLevelPassed = GameStateManager.instance.levelsUnlocked[currentLevel];

                if (currentLevelPassed || godMode)
                {
                    newLevelSelectButton.locked.enabled = false;
                    newLevelSelectButton.currentLevel.enabled = true;

                    Text currentButtonText = newLevelSelectButton.currentLevel;
                    currentButtonText.text = (currentLevel + 1).ToString();
                    
                    if (newLevelSelectButton.GetComponent<Button>() == null)
                    { Debug.Log("Warning! No button detected!"); }
                    else
                    {
                        //need to pass in current level as a temp variable or else callback will only see the last level and set all of the 
                        //level select buttons as the last level
                        int currentLevelIndex = currentLevel + 2; 
                        newLevelSelectButton.GetComponent<Button>().onClick.AddListener( () => { p_ls.Select(currentLevelIndex); });
                    }                        
                }

                else
                {
                    newLevelSelectButton.locked.enabled = true; // when enabled, the lock sprite will appear on top of the level select
                    newLevelSelectButton.currentLevel.enabled = false;
                    
                    if (newLevelSelectButton.GetComponent<Button>() == null)
                    { Debug.Log("Warning! No button detected!"); }
                }                
            }
        }
    }

}
