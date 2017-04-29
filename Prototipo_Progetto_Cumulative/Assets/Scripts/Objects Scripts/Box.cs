﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour 
{
	public GameObject[] droppables;

	public GameObject HappyMask;
	public GameObject SadMask;
	public GameObject Key;

	public GameObject RedStage;
	public GameObject GreenStage;

	public float xOffset = 0f;
	public float yOffset = 0.8f;

	public bool canInteract(int ID)
	{
		if( ID == (int)IDList.ID.Hammer)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void DropItems ()
	{
		int identifier = 0;
		foreach (var item in droppables) {
			identifier = item.GetComponent<Identifier> ().ID;
			switch (identifier) {
			case 1:
				DropHappyMask ();
				break;
			case 2:
				DropSadMask ();
				break;
			case 4:
				DropKey ();
				break;
			}
		}
	}

	public void DropHappyMask()
	{
		GameObject newMask = Instantiate (HappyMask, new Vector3 (this.transform.position.x + xOffset, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
		if(RedStage.activeInHierarchy)
		{
			newMask.transform.parent = RedStage.transform;
		}
		else if (GreenStage.activeInHierarchy)
		{
			newMask.transform.parent = GreenStage.transform;
		}
	}

	public void DropSadMask()
	{
		GameObject newMask = Instantiate (SadMask, new Vector3 (this.transform.position.x + xOffset +1, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
		if(RedStage.activeInHierarchy)
		{
			newMask.transform.parent = RedStage.transform;
		}
		else if (GreenStage.activeInHierarchy)
		{
			newMask.transform.parent = GreenStage.transform;
		}
	}

	public void DropKey()
	{
		GameObject newHammer = Instantiate (Key, new Vector3 (this.transform.position.x + xOffset -1, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
		if(RedStage.activeInHierarchy)
		{
			newHammer.transform.parent = RedStage.transform;
		}
		else if (GreenStage.activeInHierarchy)
		{
			newHammer.transform.parent = GreenStage.transform;
		}
	}
}