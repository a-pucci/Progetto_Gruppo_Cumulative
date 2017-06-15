using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
	private static List<string> Level = new List<string> () {"MenuScene", "Livello_1", "Livello_2", "Livello_3", "Livello_4", "Livello_5",
		"Livello_6", "Livello_7", "Livello_8", "Livello_9", "Livello_10", "Livello_11", "Livello_12", "Livello_13", "Test", "Test"};

	public static  void GoNextLevel()
	{
		string currentScene = SceneManager.GetActiveScene ().name;
		int sceneIndex = Level.IndexOf (currentScene);
		SceneManager.LoadScene (Level [sceneIndex + 1]);

	}
}
