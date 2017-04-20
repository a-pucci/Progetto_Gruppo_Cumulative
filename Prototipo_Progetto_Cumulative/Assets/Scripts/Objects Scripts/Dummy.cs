using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour 
{
	public float XMaskOffset = 0f;
	public float YMaskOffset = -0.4f;


	public void PutMask (GameObject mask)
	{
		mask.transform.position = new Vector3 (this.transform.position.x + XMaskOffset, this.transform.position.y + YMaskOffset, this.transform.position.z);
	}

	public bool canInteract(int ID)
	{
		if( ID == (int)IDList.ID.HappyMask || ID == (int)IDList.ID.SadMask)
		{
			return true;
		}
		else
		{
			return false;
		}
	}		
}