using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadSplashScreen()
	{
		SceneManager.LoadSceneAsync(0);
		foreach (var o in FindObjectsOfType<GameObject>()) {
			Destroy(o);
		}
	}
}
