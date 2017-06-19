using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : StageObject 
{
	[Header("-- DUMMY --")]
	[Header("Wear Offset")]
	public float XMaskOffset = 0f;
	public float YMaskOffset = -0.4f;

	private GameObject _happyStage;
	private GameObject _sadStage;
	private GameObject[] _pickups;
	private GameObject[] _interactive;

	private GameObject _currentMask;
	private PlayerPickup _player;

	void Start()
	{
		base.ID = (int)IDList.ID.Dummy;

		_pickups = GameObject.FindGameObjectsWithTag ("Pickup");
		_interactive = GameObject.FindGameObjectsWithTag ("Interactive");
		_happyStage = GameObject.FindGameObjectWithTag ("HappyStage");
		_sadStage = GameObject.FindGameObjectWithTag ("SadStage");
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerPickup> ();
	}

	public void PutMask (GameObject mask)
	{
		_currentMask = mask;
		mask.GetComponent<Collider2D> ().enabled = false;
		mask.transform.position = new Vector3 (this.transform.position.x + XMaskOffset, this.transform.position.y + YMaskOffset, this.transform.position.z);
	}

	public GameObject LoseMask ()
	{
		_currentMask.GetComponent<Collider2D> ().enabled = true;
		return _currentMask;
	}

	public override void Interact (ref GameObject other)
	{
		if(CanInteract (other))
		{
			other.SetActive (true);
			PutMask (other);
			changeObjectsStage ();
			_player.RemoveItemFromInventory ();
		}
	}

	public override bool CanInteract (GameObject other)
	{
		bool canInteract = false;
		if(other.GetComponent<StageObject> () != null)
		{
			int otherID = other.GetComponent<StageObject> ().ID;
			if(otherID == (int)IDList.ID.Mask)
			{
				canInteract = true;
			}		
		}
		return canInteract;
	}

	private void changeObjectsStage ()
	{
		for(int i = 0; i < _pickups.Length; i++)
		{
			if(_pickups[i].GetComponent<StageObject> ().canSwapStage)
			{
				if(_pickups[i].transform.parent == _happyStage.transform)
				{
					_pickups [i].transform.parent = _sadStage.transform;
				}
				else if(_pickups[i].transform.parent == _sadStage.transform)
				{
					_pickups[i].transform.parent = _happyStage.transform;
				}
			}
		}

		for(int i = 0; i < _interactive.Length; i++)
		{
			if(_interactive[i].GetComponent<StageObject> ().canSwapStage)
			{
				if(_interactive[i].transform.parent == _happyStage.transform)
				{
					_interactive [i].transform.parent = _sadStage.transform;
				}
				else if (_interactive[i].transform.parent == _sadStage.transform)
				{
					_interactive[i].transform.parent = _happyStage.transform;
				}
			}
		}
	}
}