using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : StageObject 
{
	[Header("-- BOX --")]
	public bool isMetallic;
	public GameObject[] Droppables;

	[Header("Drop Prefab")]
	public GameObject Mask;
	public GameObject Torch;
	public GameObject Gear;

	[Header("Drop Offset")]
	public float xOffset = 0f;
	public float yOffset = 0.8f;

	private GameObject _happyStage;
	private GameObject _sadStage;

	void Start()
	{
		_happyStage = GameObject.FindGameObjectWithTag ("HappyStage");
		_sadStage = GameObject.FindGameObjectWithTag ("SadStage");
	}

	public void DropItems ()
	{
		int identifier = 0;
		for(int i = 0; i < Droppables.Length; i++)
		{
			identifier = Droppables[i].GetComponent<StageObject>().ID;
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
		if(_happyStage.activeInHierarchy)
		{
			newMask.transform.parent = _happyStage.transform;
		}
		else if (_sadStage.activeInHierarchy)
		{
			newMask.transform.parent = _sadStage.transform;
		}
	}

	public void DropTorch()
	{
		GameObject newTorch = Instantiate (Torch, new Vector3 (this.transform.position.x + xOffset -1, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
		if(_happyStage.activeInHierarchy)
		{
			newTorch.transform.parent = _happyStage.transform;
		}
		else if (_sadStage.activeInHierarchy)
		{
			newTorch.transform.parent = _sadStage.transform;
		}
	}

	public void DropGear()
	{
		GameObject newGear = Instantiate (Gear, new Vector3 (this.transform.position.x + xOffset -1, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
		if(_happyStage.activeInHierarchy)
		{
			newGear.transform.parent = _happyStage.transform;
		}
		else if (_sadStage.activeInHierarchy)
		{
			newGear.transform.parent = _sadStage.transform;
		}
	}
}
