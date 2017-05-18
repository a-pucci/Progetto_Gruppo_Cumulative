using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class StageSwapController : MonoBehaviour 
{

	[Header("Timed Switch")]
	public bool TimeEnabled;
	public float SwitchTime;

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

		if (TimeEnabled) {
			StartCoroutine (SwitchCoroutine ());
		}
	}

	void Update () 
	{
		if (!TimeEnabled) {
			_swap = CrossPlatformInputManager.GetButtonDown ("Fire1"); 
			if (_swap && !_playerHealth.PlayerDead) {
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
		while (!_playerHealth.PlayerDead) {
			yield return new WaitForSecondsRealtime (SwitchTime);
			if (!_playerHealth.PlayerDead) {
				StageSwap ();
			}
		}
	}
}