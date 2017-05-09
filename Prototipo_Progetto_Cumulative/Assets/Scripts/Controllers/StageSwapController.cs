using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class StageSwapController : MonoBehaviour {

	//[SerializeField] private int maxSwapChances = 4;
	//[ReadOnly] public int currentSwapChances;

	private GameObject _redStage;
	private GameObject _greenStage;


	private GameObject _player;
	private HealthManager _playerHealth;

	private bool _swap = false;
	private bool _toggle = false;

	public Text switchText;

	public bool timeEnabled;
	public float switchTime;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag ("Player");
		_playerHealth = _player.GetComponent <HealthManager> ();

		_redStage = GameObject.FindGameObjectWithTag ("RedStage");
		_redStage.SetActive (false);

		_greenStage = GameObject.FindGameObjectWithTag ("GreenStage");
		_greenStage.SetActive (true);

		if (timeEnabled) {
			StartCoroutine (SwitchCoroutine ());
		}

		//currentSwapChances = maxSwapChances;
		//switchText.text = "Switches: " + currentSwapChances;
	}

	void Update () 
	{
		if (!timeEnabled) {
			_swap = CrossPlatformInputManager.GetButtonDown ("Fire1"); 
			if (_swap /*&& currentSwapChances > 0 */ && !_playerHealth.playerDead) {
				StageSwap ();
				//currentSwapChances--;
				//switchText.text = "Switches: " + currentSwapChances;
			}
		}
	}

	private void StageSwap ()
	{

		if (_toggle) {
			_greenStage.SetActive (true);
			_redStage.SetActive (false);
		} 
		else 
		{
			_greenStage.SetActive (false);
			_redStage.SetActive (true);
		}
		_toggle = !_toggle;
	}


	IEnumerator SwitchCoroutine () {
		while (!_playerHealth.playerDead) {
			yield return new WaitForSecondsRealtime (switchTime);
			if (!_playerHealth.playerDead) {
				StageSwap ();
			}
		}
	}
}