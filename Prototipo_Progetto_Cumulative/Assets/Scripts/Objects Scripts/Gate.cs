using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gate : StageObject 
{
	[SerializeField] private bool _openRight;
	[SerializeField] private float _rotationSpeed;

	[Header("Gate Audio")]
	public AudioClip GateClip;
	[Range(0.0f, 1.0f)] public float GateVolume = 0.8f;

	private SFXController _sfxManager;
	private bool _open = false;
	private bool _close = false;
	private bool _isMoving = false;

	void Start()
	{
		base.ID = (int)IDList.ID.Gate;
		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
	}

	void Update()
	{
		if(_open)
		{
			OpeningGate ();
		}
		if(_close)
		{
			ClosingGate ();
		}
	}

	public void openGate()
	{
		_open = true;
		_close = false;
	}

	public void closeGate()
	{
		_close = true;
		_open = false;
	}

	private void OpeningGate()
	{
		if(_openRight)
		{
			if(transform.rotation.z < 0.705)
			{
				if (_isMoving == false && !_sfxManager.IsPlaying("gate")) {
					_sfxManager.PlaySFX (GateClip, GateVolume, "gate");
				}
				transform.Rotate (Vector3.forward * _rotationSpeed * Time.deltaTime);
				_isMoving = true;

			}
			else
			{
				if (_isMoving == true && _sfxManager.IsPlaying("gate")) {
					_sfxManager.StopSound("gate");
				}
				_open = false;
				_isMoving = false;
			}
		}
		else
		{
			if(transform.rotation.z > -0.705)
			{
				if (_isMoving == false && !_sfxManager.IsPlaying("gate")) {
					_sfxManager.PlaySFX (GateClip, GateVolume, "gate");
				}
				transform.Rotate (Vector3.back * _rotationSpeed * Time.deltaTime);
				_isMoving = true;
			}
			else
			{
				if (_isMoving == true && _sfxManager.IsPlaying("gate")) {
					_sfxManager.StopSound("gate");
				}
				_open = false;
				_isMoving = false;
			}
		}
	}

	private void ClosingGate()
	{
		if(_openRight)
		{
			if(transform.rotation.z > 0)
			{
				transform.Rotate (Vector3.back * _rotationSpeed * Time.deltaTime);
			}
			else
			{
				_close = false;
			}
		}
		else
		{
			if(transform.rotation.z < 0)
			{
				transform.Rotate (Vector3.forward * _rotationSpeed * Time.deltaTime);
			}
			else
			{
				_close = false;
			}
		}
	}
}
