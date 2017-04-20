using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torso : MonoBehaviour 
{
	public float XMaskOffset = 0.4f;
	public float YMaskOffset = 2f;


	public void PutMask (GameObject mask)
	{
		Instantiate (mask, new Vector3(this.transform.position.x + XMaskOffset, this.transform.position.y + YMaskOffset, this.transform.position.z), this.transform.rotation);
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