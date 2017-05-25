using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{

	public string firstScene;

	public void StartGame()
	{
		if (firstScene.Length < 0) {
			Debug.LogError ("Scene to load not selected!");
		} else {
			SceneManager.LoadScene (firstScene);
		}
	}

	public void LevelSelect()
	{
		Debug.LogError ("TODO");
	}

	public void QuitGame()
	{
		Debug.Log ("Quitting Game");
		Application.Quit ();
	}

}
