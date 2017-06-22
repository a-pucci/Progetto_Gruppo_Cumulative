using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager
{
	private static List<string> Level = new List<string> () {"MenuScene", "Storia_1", "Livello_1", "Storia_2", "Livello_2", "Storia_3", "Livello_3", 
		"Storia_4", "Livello_4", "Storia_5", "Livello_5", "Storia_6", "Livello_6", "Storia_7", "Livello_7", "Storia_8", "Livello_8",
		"Storia_9", "Livello_9", "Storia_10", "Livello_10", "Storia_11", "Livello_11", "Storia_12", "Livello_12", "Storia_13", "Livello_13",
		"Finale_Buono", "MenuScene", "Finale_Cattivo", "MenuScene", "Test", "Test"};

	public static  void GoNextLevel()
	{
		string currentScene = SceneManager.GetActiveScene ().name;
		int sceneIndex = Level.IndexOf (currentScene);

		if(currentScene == "Livello_13")
		{
			
		}
		SceneManager.LoadScene (Level [sceneIndex + 1]);

	}
}
