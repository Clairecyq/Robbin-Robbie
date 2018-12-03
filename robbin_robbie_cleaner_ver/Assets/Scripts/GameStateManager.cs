using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager instance;

    public int totalNumLevels = 27;
    public int levelStartingIndex;

    public string levelDescription;

    public bool[] levelsUnlocked;

    public Vector3[] badges;
    public bool isInitialized;

    private int l_c;
    private int c_c;

    private int t_c;

    public void Initialize(int totalNumLevels)
    {   
        this.totalNumLevels = totalNumLevels;
        levelsUnlocked = new bool[totalNumLevels];
        badges = new Vector3[totalNumLevels];
        levelsUnlocked[0] = true;
        isInitialized = true;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject); // Prevent the game controller from being destroyed accidentally.
    }

    public int getBadgeCount(int num) {
        if (num == 1) {
            return c_c;
        }
        else if (num == 2) {
            return t_c;
        }
        else {
            return l_c;
        }
    }

    public void winLevel() {
        l_c += 1;
    }

    public void allCandy() {
        c_c += 1;
    }

    public void fastTime() {
        t_c += 1;
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
