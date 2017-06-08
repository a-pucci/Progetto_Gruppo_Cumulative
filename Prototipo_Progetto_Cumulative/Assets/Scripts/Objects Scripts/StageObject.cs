using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageObject : MonoBehaviour 
{
	[Header("-- GENERIC --")]
	public int ID;
	public bool canSwapStage;

	public virtual GameObject Pickup () {return null;}
	public virtual void Interact (ref GameObject other) {}
	public virtual bool CanInteract (GameObject other) {return false;}
	public virtual bool IsDestroyedOnUse () {return false;}
}
