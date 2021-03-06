﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraStart : MonoBehaviour 
{
	public Transform[] Targets;
	public float Speed;
	public float WaitTime;
	public float CurtainsWait;

	private Transform _cameraTrans;
	private bool _showingObjects;
	private Vector3 _target;

	private int _counter = 0;
	private bool _waiting = false;
	private bool _curtainsOpen = false;

	private PlayerUserController _player;

	public GameObject _happyStage;
	public GameObject _sadStage;

	private StageSwapController _stageSwap;
	private bool _skip = false;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerUserController> ();
		_stageSwap = GameObject.FindGameObjectWithTag ("StageSwap").GetComponent<StageSwapController> ();
		_cameraTrans = this.transform;
		_showingObjects = true;	

		if(Targets.Length > 0)
		{
			_target = GetTargetPosition (Targets [_counter].position);
		}

		_stageSwap.LockSwap ();
		_player.CanMove = false;
		StartCoroutine (InitialWait ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(CrossPlatformInputManager.GetButtonDown("Drop"))
		{
			_skip = true;
		}
		if(_showingObjects && _curtainsOpen)
		{	
			Move ();
		}			
	}

	private void Move()
	{
		if(_counter < Targets.Length && !_skip)
		{
			_cameraTrans.position = Vector3.MoveTowards (_cameraTrans.position, _target, Speed * Time.deltaTime);

			if (_cameraTrans.position == _target && !_waiting)
			{ 	
				CheckStage ();
				StartCoroutine (TargetReached ());
			}
		}
		else
		{
			StopCoroutine (TargetReached ());
			_showingObjects = false;
			_player.CanMove = true;
			_stageSwap.UnlockSwap ();
			_happyStage.SetActive (true);
			_sadStage.SetActive (false);
		}
	}

	private IEnumerator TargetReached()
	{
		_waiting = true;
		CheckStage ();
		yield return new WaitForSeconds (WaitTime);

		if(_counter < Targets.Length)
		{
			_counter++;
			if(_counter < Targets.Length)
			{
				_target = GetTargetPosition (Targets [_counter].position);
			}
		}
		_waiting = false;
	}

	private IEnumerator InitialWait()
	{
		yield return new WaitForSeconds (CurtainsWait);
		_curtainsOpen = true;
	}

	public bool IsShowing()
	{
		return _showingObjects;
	}

	private void CheckStage()
	{
		if(Targets[_counter].IsChildOf(_happyStage.transform))
		{
			_happyStage.SetActive (true);
			_sadStage.SetActive (false);
		}
		else if(Targets[_counter].IsChildOf(_sadStage.transform))
		{
			_happyStage.SetActive (false);
			_sadStage.SetActive (true);
		}
	}

	private Vector3 GetTargetPosition(Vector3 TargetPos)
	{
		Vector3 target;
		target.x = TargetPos.x;
		target.y = TargetPos.y;
		target.z = -10;

		return target;
	}

}
