using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

	public SceneFader fader;
	public Button[] levelButtons;
    public int totalNumLevels;

    void Start()
    {
        totalNumLevels = 12;

        LoggingManager.instance.Initialize(626, 0, false);
        LoggingManager.instance.RecordPageLoad("Game Instance Manager Started");

        if (!GameStateManager.instance.isInitialized )
        {
            GameStateManager.instance.Initialize(totalNumLevels);
            GameStateManager.instance.isInitialized = true;
        }

    }

    public void Select (int levelIndex)
	{
		fader.FadeTo(levelIndex);
	}

}
