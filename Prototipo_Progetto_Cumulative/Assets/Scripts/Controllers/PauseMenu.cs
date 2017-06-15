using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PauseMenu : MonoBehaviour {

	[SerializeField] private bool paused = false;
	public string menuScene;
	private Canvas thisCanvas;

	public void ResumeGame()
	{
		thisCanvas.enabled = false;
		paused = false;
		Time.timeScale = 1;
	}

	public void RestartGame()
	{
		thisCanvas.enabled = false;
		paused = false;
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void StopGame()
	{
		thisCanvas.enabled = true;
		paused = true;
		Time.timeScale = 0;
	}

	public void ReturnToMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene (menuScene);
	}

	public void QuitGame()
	{
		Debug.Log ("Quitting Game");
		Application.Quit ();
	}

	void Start() {
		thisCanvas = this.gameObject.GetComponent<Canvas> ();
		thisCanvas.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown("Pause")) {
			if (paused) {
				ResumeGame ();
			} else {
				StopGame ();
			}
		}
	}
}
