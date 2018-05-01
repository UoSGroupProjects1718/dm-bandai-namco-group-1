using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour {
	public void SwitchToGameScene()
	{
		SceneManager.LoadScene("Main");
	}
}
