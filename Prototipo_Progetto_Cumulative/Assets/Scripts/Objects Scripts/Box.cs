using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : StageObject 
{
	public GameObject[] droppables;

	public GameObject Mask;
	public GameObject Torch;
	public GameObject Gear;

	public GameObject HappyStage;
	public GameObject SadStage;

	public float xOffset = 0f;
	public float yOffset = 0.8f;

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
		if(HappyStage.activeInHierarchy)
		{
			newMask.transform.parent = HappyStage.transform;
		}
		else if (SadStage.activeInHierarchy)
		{
			newMask.transform.parent = SadStage.transform;
		}
	}

	public void DropTorch()
	{
		GameObject newTorch = Instantiate (Torch, new Vector3 (this.transform.position.x + xOffset -1, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
		if(HappyStage.activeInHierarchy)
		{
			newTorch.transform.parent = HappyStage.transform;
		}
		else if (SadStage.activeInHierarchy)
		{
			newTorch.transform.parent = SadStage.transform;
		}
	}

	public void DropGear()
	{
		GameObject newGear = Instantiate (Gear, new Vector3 (this.transform.position.x + xOffset -1, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
		if(HappyStage.activeInHierarchy)
		{
			newGear.transform.parent = HappyStage.transform;
		}
		else if (SadStage.activeInHierarchy)
		{
			newGear.transform.parent = SadStage.transform;
		}
	}
}
