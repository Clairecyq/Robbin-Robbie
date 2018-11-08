using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager instance;

    public int totalNumLevels = 12;
    public int levelStartingIndex;

    public string levelDescription;

    public bool[] levelsUnlocked;
    public bool isInitialized;

    public void Initialize(int totalNumLevels)
    {   
        this.totalNumLevels = totalNumLevels;
        levelsUnlocked = new bool[totalNumLevels];
        levelsUnlocked[0] = true;
        isInitialized = true;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject); // Prevent the game controller from being destroyed accidentally.
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }      
    }
}
