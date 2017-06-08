using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : StageObject
{
	[Header("-- GEAR --")]
	public float RotationSpeed;

	private bool _rotateRight = false;
	private bool _rotateLeft = false;

	private bool _isDestroyedOnUse = false;

	void Start()
	{
		base.ID = (int)IDList.ID.Gear;
		base._sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
	}

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

	public override GameObject Pickup ()
	{
		base._sfxManager.PlaySFX (base.PickupClip);
		return this.gameObject;
	}

	public override bool CanInteract (GameObject other)
	{
		bool canInteract = false;
		if(other.GetComponent<StageObject> () != null)
		{
			int otherID = other.GetComponent<StageObject> ().ID;
			if(otherID == (int)IDList.ID.Mechanism)
			{
				canInteract = true;
			}		
		}
		return canInteract;
	}

	public override bool IsDestroyedOnUse ()
	{
		return _isDestroyedOnUse;
	}
}
