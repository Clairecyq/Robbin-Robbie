using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNavButtonBar : MonoBehaviour {

    [SerializeField]
    private LevelNavButtonBar levelNavButtonBar;
    public Button pauseButton;
    public Button restartButton;
    public Button homeButton;
    public Button muteButton;
    public Sprite musicOn;
    public Sprite musicMuted;

    // Use this for initialization
    void Start () {

        pauseButton.onClick.AddListener((UnityEngine.Events.UnityAction)GameController.instance.PauseOrResume);
        restartButton.onClick.AddListener((UnityEngine.Events.UnityAction)GameController.instance.Restart);
        homeButton.onClick.AddListener((UnityEngine.Events.UnityAction)GameController.instance.ChangeToHome);
        muteButton.onClick.AddListener((UnityEngine.Events.UnityAction)GameController.instance.MuteMusic);

    }

    // Update is called once per frame
    void Update () {
        
		if (AudioListener.pause)
        {
            muteButton.image.sprite = musicMuted;
        }

        else 
        {
            muteButton.image.sprite = musicOn;
        }
    }
}
