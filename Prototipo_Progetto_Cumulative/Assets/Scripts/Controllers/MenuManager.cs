using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{

	public string firstScene;
	public GameObject mainMenu;
	public GameObject levelMenu;

	public void StartGame()
	{
		if (firstScene.Length < 0) 
		{
			Debug.LogError ("Scene to load not selected!");
		} 
		else 
		{
			LevelManager.GoNextLevel ();
		}
	}

	public void LevelSelect()
	{
		mainMenu.SetActive (false);
		levelMenu.SetActive (true);
	}

	public void MainMenu()
	{
		mainMenu.SetActive (true);
		levelMenu.SetActive (false);
	}

	public void LoadLevel(string level)
	{
		SceneManager.LoadScene (level);
	}

	public void QuitGame()
	{
		Debug.Log ("Quitting Game");
		Application.Quit ();
	}

}
