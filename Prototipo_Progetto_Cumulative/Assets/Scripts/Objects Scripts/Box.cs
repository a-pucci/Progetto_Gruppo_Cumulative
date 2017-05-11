using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : StageObject 
{
	public GameObject[] droppables;

	public GameObject Mask;
	public GameObject Torch;
	public GameObject Gear;

	public GameObject RedStage;
	public GameObject GreenStage;

	public float xOffset = 0f;
	public float yOffset = 0.8f;

//	public bool canInteract(int ID)
//	{
//		if( ID == (int)IDList.ID.Hammer)
//		{
//			return true;
//		}
//		else
//		{
//			return false;
//		}
//	}

	public void DropItems ()
	{
		int identifier = 0;
		for(int i = 0; i < droppables.Length; i++)
		{
			identifier = droppables[i].GetComponent<StageObject>().ID;
			switch (identifier) 
			{
			case 1:
				DropMask ();
				break;			
			case 3: 
				DropGear ();
				break;
			case 4:
				DropTorch ();
				break;

			}
		}
	}

	public void DropMask()
	{
		GameObject newMask = Instantiate (Mask, new Vector3 (this.transform.position.x + xOffset, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
		if(RedStage.activeInHierarchy)
		{
			newMask.transform.parent = RedStage.transform;
		}
		else if (GreenStage.activeInHierarchy)
		{
			newMask.transform.parent = GreenStage.transform;
		}
	}

	public void DropTorch()
	{
		GameObject newTorch = Instantiate (Torch, new Vector3 (this.transform.position.x + xOffset -1, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
		if(RedStage.activeInHierarchy)
		{
			newTorch.transform.parent = RedStage.transform;
		}
		else if (GreenStage.activeInHierarchy)
		{
			newTorch.transform.parent = GreenStage.transform;
		}
	}

	public void DropGear()
	{
		GameObject newGear = Instantiate (Gear, new Vector3 (this.transform.position.x + xOffset -1, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
		if(RedStage.activeInHierarchy)
		{
			newGear.transform.parent = RedStage.transform;
		}
		else if (GreenStage.activeInHierarchy)
		{
			newGear.transform.parent = GreenStage.transform;
		}
	}
}
