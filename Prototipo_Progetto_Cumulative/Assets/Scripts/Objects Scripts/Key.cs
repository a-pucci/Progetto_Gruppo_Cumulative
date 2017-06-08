using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : StageObject
{
	public GameObject KeyPrefab;
	public AudioClip KeyPickupClip;

	private bool _isDestroyedOnUse = true;

	void Start()
	{
		base.ID = (int)IDList.ID.Key;
		base._sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
	}

	public override GameObject Pickup ()
	{
		base._sfxManager.PlaySFX (KeyPickupClip);
		return KeyPrefab;
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
