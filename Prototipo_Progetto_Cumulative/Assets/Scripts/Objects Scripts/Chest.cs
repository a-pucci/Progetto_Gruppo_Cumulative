using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour 
{
	public GameObject HappyMask;
	public GameObject SadMask;
	public GameObject Hammer;

	public float xOffset = 0f;
	public float yOffset = -0.3f;

	public bool canInteract(int ID)
	{
		if( ID == (int)IDList.ID.Key)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void DropHappyMask()
	{
		Instantiate (HappyMask, new Vector3 (this.transform.position.x + xOffset, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
	}

	public void DropSadMask()
	{
		Instantiate (SadMask, new Vector3 (this.transform.position.x + xOffset +1, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
	}

	public void DropHammer()
	{
		Instantiate (Hammer, new Vector3 (this.transform.position.x + xOffset -1, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
	}
}
