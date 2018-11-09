using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

	public SceneFader fader;
	public Button[] levelButtons;
    public int totalNumLevels;

    void Start()
    {
        if (!GameStateManager.instance.isInitialized )
        {
            GameStateManager.instance.Initialize(totalNumLevels);
            GameStateManager.instance.isInitialized = true;
        }

    }

    public void Select (int levelIndex)
	{
        AudioListener.pause = false;
        fader.FadeTo(levelIndex);
	}

}
