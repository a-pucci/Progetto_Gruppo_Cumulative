using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : StageObject
{
	void Start()
	{
		base.ID = (int)IDList.ID.Mask;
	}

	public override GameObject Pickup ()
	{
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
}
