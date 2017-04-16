using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ColorSwapController : MonoBehaviour {

	//[SerializeField] private int maxSwapChances = 4;
	//[ReadOnly] public int currentSwapChances;

	private GameObject[] redPlatforms;
	private GameObject[] greenPlatforms;

	private GameObject _redBG;
	private GameObject _greenBG;

	private GameObject _player;
	private HealthManager _playerHealth;

	private bool _swap = false;
	private bool _toggle = false;

	public Text switchText;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag ("Player");
		_playerHealth = _player.GetComponent <HealthManager> ();

		_redBG = GameObject.FindGameObjectWithTag ("RedBackground");
		_redBG.SetActive (false);

		_greenBG = GameObject.FindGameObjectWithTag ("GreenBackground");
		_greenBG.SetActive (true);

		greenPlatforms = GameObject.FindGameObjectsWithTag ("GreenPlatform");
		for (int i = 0; i < greenPlatforms.Length; i++) 
		{
			greenPlatforms [i].SetActive (true);
		}

		redPlatforms = GameObject.FindGameObjectsWithTag ("RedPlatform");
		for(int i = 0; i < redPlatforms.Length; i++ )
		{
			redPlatforms [i].SetActive (false);
		}

		//currentSwapChances = maxSwapChances;
		//switchText.text = "Switches: " + currentSwapChances;

	}

	void Update () 
	{

		_swap = CrossPlatformInputManager.GetButtonDown ("Fire1"); 
		if(_swap /*&& currentSwapChances > 0 */&& !_playerHealth.playerDead)
		{
			ColorSwap ();
			//currentSwapChances--;
			//switchText.text = "Switches: " + currentSwapChances;
		}

	}
		
	void ColorSwap ()
	{

		if (_toggle) {
			_greenBG.SetActive (true);
			_redBG.SetActive (false);

			for (int i = 0; i < greenPlatforms.Length; i++) {
				greenPlatforms [i].SetActive (true);
			}

			for (int i = 0; i < redPlatforms.Length; i++) {
				redPlatforms [i].SetActive (false);
			}

		} 
		else 
		{
			_greenBG.SetActive (false);
			_redBG.SetActive (true);

			for (int i = 0; i < greenPlatforms.Length; i++) {
				greenPlatforms [i].SetActive (false);
			}
				
			for (int i = 0; i < redPlatforms.Length; i++) {
				redPlatforms [i].SetActive (true);
			}

		}
		_toggle = !_toggle;
	}

}
