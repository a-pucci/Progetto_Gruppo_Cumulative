using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : StageObject 
{
	[Header("-- DUMMY --")]
	[Header("Wear Offset")]
	public float XMaskOffset = 0f;
	public float YMaskOffset = -0.4f;

	private GameObject _currentMask;
	private bool _hasMask = false;


	public void PutMask (GameObject mask)
	{
		_hasMask = true;
		_currentMask = mask;
		mask.GetComponent<Collider2D> ().enabled = false;
		mask.transform.position = new Vector3 (this.transform.position.x + XMaskOffset, this.transform.position.y + YMaskOffset, this.transform.position.z);
	}

	public GameObject LoseMask ()
	{
		_currentMask.GetComponent<Collider2D> ().enabled = true;
		_hasMask = false;
		return _currentMask;
	}
		
	public bool HasMask ()
	{
		return _hasMask;
	}
}