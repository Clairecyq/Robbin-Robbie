using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

	public SceneFader fader;
	public Button[] levelButtons;

    void Start()
    {
        LoggingManager.instance.Initialize(626, 0, false);
        LoggingManager.instance.RecordPageLoad("Game Instance Manager Started");

        // creating our initialization here - change to FALSE in release

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached) {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void Select (int levelIndex)
	{
		fader.FadeTo(levelIndex);
	}

}
