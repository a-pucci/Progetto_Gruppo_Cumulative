using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gate : StageObject 
{
	[SerializeField] private bool _openRight;
	[SerializeField] private float _rotationSpeed;

	private bool _open = false;
	private bool _close = false;

	void Start()
	{
		base.ID = (int)IDList.ID.Gate;
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
				transform.Rotate (Vector3.forward * _rotationSpeed * Time.deltaTime);
			}
			else
			{
				_open = false;
			}
		}
		else
		{
			if(transform.rotation.z > -0.705)
			{
				transform.Rotate (Vector3.back * _rotationSpeed * Time.deltaTime);
			}
			else
			{
				_open = false;
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
