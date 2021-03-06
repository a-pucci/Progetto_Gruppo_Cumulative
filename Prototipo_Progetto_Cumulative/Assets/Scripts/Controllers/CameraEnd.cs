﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraEnd : MonoBehaviour 
{
	[Header("Camera Setting")]
	public float MovingSpeed;
	public float MaxSize;
	public Animator CameraAC;

	[Header("UI Settings")]
	public GameObject[] Interface;
	public Animator LeftCurtainAC;
	public Animator RightCurtainAC;

	[Header("Level Audio")]
	public AudioClip EndLevelClip;
	[Range(0.0f, 1.0f)] public float EndLevelVolume = 1f;
	public AudioClip StartLevelClip;
	[Range(0.0f, 1.0f)] public float StartLevelVolume = 1f;

	private float _closureTime = 2f;

	private Camera _camera;
	private Transform _cameraTransform;

	private bool _moving = false;
	private bool _isShowing = false;
	private bool _closing = false;

	private float _initialSize;
	private Vector3 _initialPosition;
	private Vector3 _destination;

	private float _startTime;
	private float _journeyLenght;

	private SFXController _sfxManager;

	// Use this for initialization
	void Start () 
	{
		_camera = Camera.main;
		_cameraTransform = this.gameObject.transform;
		_initialSize = _camera.orthographicSize;

		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
		_sfxManager.PlaySFX (StartLevelClip, StartLevelVolume);
	}

	void Update()
	{
		if(_moving)
		{
			float distanceCovered = (Time.time - _startTime) * MovingSpeed;
			float fractionJourney = distanceCovered / _journeyLenght;

			_cameraTransform.position = Vector3.Lerp (_initialPosition, _destination, fractionJourney);
			_camera.orthographicSize = Mathf.Lerp (_initialSize, MaxSize, fractionJourney);

			if (this.gameObject.transform.position == _destination && _camera.orthographicSize == MaxSize)
			{
				_isShowing = false;
				LeftCurtainAC.SetTrigger ("Close");
				RightCurtainAC.SetTrigger ("Close");
			}
		}
	}

	public void StartClose ()
	{
		_sfxManager.PlaySFX (EndLevelClip, EndLevelVolume);
		for(int i = 0; i < Interface.Length; i++)
		{
			Interface[i].SetActive (false);
		}

		_moving = true;
		_isShowing = true;
		_closing = true;

		_initialPosition = _cameraTransform.position;
		_destination = new Vector3 (0, 0, -10);
		_startTime = Time.time;
		_journeyLenght = Vector3.Distance (_initialPosition, _destination);
		StartCoroutine (ChangeScene ());
	}

	public bool closingAnimation()
	{
		return _closing;
	}

	private IEnumerator ChangeScene()
	{

		while (_isShowing == true)
		{
			yield return new WaitForSeconds (0);
		}
		yield return new WaitForSeconds (_closureTime);
		LevelManager.GoNextLevel();
	}
}