using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : StageObject 
{
	public float time;
	public bool shooted;
	private bool _isDestroyedOnUse = true;

	void Start()
	{
		base.ID = (int)IDList.ID.CannonBall;
	}

	void OnTriggerEnter2D(Collider2D other)
	{	
		if(other.gameObject.GetComponent<StageObject> () != null && 
			(other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Box ||
			 other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Gate))
		{
			if(shooted)
			{
				GameObject.Destroy (other.gameObject, time);
				shooted = false;
			}
		}
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
			if(otherID == (int)IDList.ID.Cannon)
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
