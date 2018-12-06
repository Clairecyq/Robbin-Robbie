using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;
    public Button playButton;

    public void Start() {
        LoggingManager.instance.Initialize(626, 12, false); // logging is active
        LoggingManager.instance.RecordPageLoad("Game Instance Manager Started");
        LoggingManager.instance.InitializeABTestValue();
    }

    public void LoadLevel(int sceneIndex)
    {
        loadingScreen.SetActive(false);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }	
    
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            playButton.gameObject.SetActive(false);
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;    
        
            yield return null;
        }

        playButton.gameObject.SetActive(false);    
    }
}
