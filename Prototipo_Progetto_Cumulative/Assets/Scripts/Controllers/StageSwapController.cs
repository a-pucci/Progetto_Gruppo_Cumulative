using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class StageSwapController : MonoBehaviour {

	public bool timeEnabled;
	public float switchTime;

	private GameObject _happyStage;
	private GameObject _sadStage;

	private GameObject _player;
	private HealthManager _playerHealth;

	private bool _swap = false;
	private bool _toggle = false;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag ("Player");
		_playerHealth = _player.GetComponent <HealthManager> ();

		_happyStage = GameObject.FindGameObjectWithTag ("HappyStage");
		_happyStage.SetActive (true);

		_sadStage = GameObject.FindGameObjectWithTag ("SadStage");
		_sadStage.SetActive (false);

		if (timeEnabled) {
			StartCoroutine (SwitchCoroutine ());
		}
	}

	void Update () 
	{
		if (!timeEnabled) {
			_swap = CrossPlatformInputManager.GetButtonDown ("Fire1"); 
			if (_swap && !_playerHealth.playerDead) {
				StageSwap ();

			}
		}
	}

	private void StageSwap ()
	{

		if (_toggle) {
			_happyStage.SetActive (true);
			_sadStage.SetActive (false);
		} 
		else 
		{
			_happyStage.SetActive (false);
			_sadStage.SetActive (true);
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