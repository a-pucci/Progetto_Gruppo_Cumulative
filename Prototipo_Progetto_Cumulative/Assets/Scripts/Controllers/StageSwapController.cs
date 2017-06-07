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

	[Header("Switch Audio")]
	public AudioClip SwitchClip;

	private GameObject _happyStage;
	private GameObject _sadStage;

	private GameObject _player;
	private HealthManager _playerHealth;

	private bool _swap = false;
	public bool toggle = false;

	private SFXController _sfxManager;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag ("Player");
		_playerHealth = _player.GetComponent <HealthManager> ();

		_happyStage = GameObject.FindGameObjectWithTag ("HappyStage");
		_happyStage.SetActive (true);

		_sadStage = GameObject.FindGameObjectWithTag ("SadStage");
		_sadStage.SetActive (false);

		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();

		if (TimeEnabled) {
			StartCoroutine (SwitchCoroutine ());
		}
	}

	void Update () 
	{
		if (!TimeEnabled && Time.timeScale > 0) {
			_swap = CrossPlatformInputManager.GetButtonDown ("Fire1"); 
			if (_swap && !_playerHealth.PlayerDead) {
				StageSwap ();

			}
		}
	}

	private void StageSwap ()
	{
		_sfxManager.PlaySFX (SwitchClip);
		if (toggle) 
		{
			_happyStage.SetActive (true);
			_sadStage.SetActive (false);
		} 
		else 
		{
			_happyStage.SetActive (false);
			_sadStage.SetActive (true);
		}
		toggle = !toggle;
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