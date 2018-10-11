using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour {
	
	// Update is called once per frame
	public void ChangeToScene (string targetScene) {
        SceneManager.LoadScene(targetScene);
	}
}
