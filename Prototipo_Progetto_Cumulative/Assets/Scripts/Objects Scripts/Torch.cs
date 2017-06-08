using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : StageObject 
{
	private bool _isDestroyedOnUse = false;

	void Start()
	{
		base.ID = (int)IDList.ID.Torch;
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
			if(otherID == (int)IDList.ID.Box || otherID == (int)IDList.ID.Cannon)
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
