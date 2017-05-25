using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : StageObject
{
	[Header("-- GEAR --")]
	public float RotationSpeed;

	private bool _rotateRight = false;
	private bool _rotateLeft = false;

	void Update()
	{
		if(_rotateRight)
		{
			transform.Rotate (Vector3.forward * Time.deltaTime * RotationSpeed);
		}
		else if(_rotateLeft)
		{
			transform.Rotate (Vector3.back * Time.deltaTime * RotationSpeed);
		}

	}

	public void RotateRight()
	{
		_rotateRight = true;
		_rotateLeft = false;
	}

	public void RotateLeft()
	{
		_rotateLeft = true;
		_rotateRight = false;
	}
}
