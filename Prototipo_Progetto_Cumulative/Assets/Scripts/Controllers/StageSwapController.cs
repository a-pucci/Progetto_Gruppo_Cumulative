using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class StageSwapController : MonoBehaviour {

	public GameObject HappyStage;
	public GameObject SadStage;

	private GameObject _player;
	private HealthManager _playerHealth;

	private bool _swap = false;
	private bool _toggle = false;

	public bool timeEnabled;
	public float switchTime;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag ("Player");
		_playerHealth = _player.GetComponent <HealthManager> ();

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
			HappyStage.SetActive (true);
			SadStage.SetActive (false);
		} 
		else 
		{
			HappyStage.SetActive (false);
			SadStage.SetActive (true);
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