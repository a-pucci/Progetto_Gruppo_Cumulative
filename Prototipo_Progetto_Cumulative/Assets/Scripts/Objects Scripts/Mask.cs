using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : StageObject
{
	private bool _isDestroyedOnUse = true;

	void Start()
	{
		base.ID = (int)IDList.ID.Mask;
		base._sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
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
			if(otherID == (int)IDList.ID.Dummy)
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
