using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerPickup : MonoBehaviour 
{
	public Text PickupText;
	public Image InventoryIcon;

	private GameObject _triggerObject;
	private GameObject _storedItem;

	private bool _pickedUp = false;


	// Use this for initialization
	void Start () 
	{
		PickupText.enabled = false;
		InventoryIcon.enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if(_triggerObject != null && CrossPlatformInputManager.GetButtonDown("Pickup"))
		{
			if (_triggerObject.CompareTag ("Pickup"))
			{
				Pickup ();
			}
			else if (_triggerObject.CompareTag("Interactive")  && _pickedUp)
			{
				Interact (_triggerObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		_triggerObject = collision.gameObject;
		PickupText.enabled = true;
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		_triggerObject = null;
		PickupText.enabled = false;
	}

	private void Pickup()
	{
		_pickedUp = true;
		InventoryIcon.enabled = true;
	
		InventoryIcon.sprite =  _triggerObject.GetComponent<SpriteRenderer> ().sprite;

		_storedItem = _triggerObject;
		_storedItem.gameObject.SetActive (false);
	}

	private void Interact(GameObject trigger)
	{
		int IDstored = _storedItem.gameObject.GetComponent<Identifier> ().ID;
		int IDcollide = trigger.GetComponent<Identifier> ().ID;

		switch (IDcollide)
		{
		case (int)IDList.ID.Torso:
			
			Torso torso = trigger.GetComponent<Torso> ();
			if(torso.canInteract(IDstored))
			{
				_storedItem.gameObject.SetActive (true);
				torso.PutMask (_storedItem);
				_storedItem.SetActive (false);
				_pickedUp = false;
				InventoryIcon.enabled = false;
			}
			break;

		case (int)IDList.ID.Box:
			
			//TODO
			break;


		case (int)IDList.ID.Chest:

			//TODO
			break;


		}
	}
}
