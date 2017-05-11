using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StageObject : MonoBehaviour 
{
	protected int ID;
	protected bool canSwapStage;

	public virtual int getID() { return ID;}
	public virtual bool canSwap() { return canSwapStage;}
}
