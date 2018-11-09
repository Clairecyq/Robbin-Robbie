using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

	public SceneFader fader;
	public Button[] levelButtons;
    public int totalNumLevels;

    void Start()
    {

        LoggingManager.instance.Initialize(626, 1, true);
        LoggingManager.instance.RecordPageLoad("Game Instance Manager Started");
        LoggingManager.instance.InitializeABTestValue();

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
