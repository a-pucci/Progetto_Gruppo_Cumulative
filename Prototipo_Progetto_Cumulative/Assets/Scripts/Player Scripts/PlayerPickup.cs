using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerPickup : MonoBehaviour 
{
	[Header("Inventory")]
	public Text PickupText;
	public Text InteractText;
	public Image InventoryIcon;

	[Header("Drop Offset")]
	public float xDropOffset = 0.0f;
	public float YDropOffset = 0.3f;

	private GameObject _happyStage;
	private GameObject _sadStage;

	private GameObject _triggerObject;
	private GameObject _storedItem;
	private GameObject[] _pickups;
	private GameObject[] _interactive;

	private bool _pickedUp = false;


	// Use this for initialization
	void Start () 
	{
		_pickups = GameObject.FindGameObjectsWithTag ("Pickup");
		_interactive = GameObject.FindGameObjectsWithTag ("Interactive");
		_happyStage = GameObject.FindGameObjectWithTag ("HappyStage");
		_sadStage = GameObject.FindGameObjectWithTag ("SadStage");

		
		PickupText.enabled = false;
		InteractText.enabled = false;
		InventoryIcon.enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if(_triggerObject != null && CrossPlatformInputManager.GetButtonDown("Pickup"))
		{
			if (_triggerObject.CompareTag ("Pickup"))
			{
				if (_pickedUp) 
				{
					Drop ();
				}

				Pickup ();
			}
			else if (_triggerObject.CompareTag("Interactive")  && _pickedUp)
			{
				Interact (_triggerObject);
			}
		}

		if(_storedItem != null && CrossPlatformInputManager.GetButtonDown("Drop"))
		{
			Drop ();
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		_triggerObject = collision.gameObject;

		if(collision.CompareTag("Pickup"))
		{
			PickupText.enabled = true;	
		}
		else if(collision.CompareTag("Interactive"))
		{
			InteractText.enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		_triggerObject = null;
		PickupText.enabled = false;
		InteractText.enabled = false;
	}

	private void Pickup()
	{
		_pickedUp = true;

		InventoryIcon.enabled = true;
		InventoryIcon.sprite =  _triggerObject.GetComponent<SpriteRenderer> ().sprite;
		_triggerObject.GetComponent<SpriteRenderer> ().enabled = true;

		_storedItem = _triggerObject;
		_storedItem.transform.parent = this.transform.parent;
		_storedItem.SetActive (false);
	}

	private void Drop ()
	{
		_storedItem.SetActive (true);
		_storedItem.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + YDropOffset, this.transform.position.z);

		if(_happyStage.activeInHierarchy)
		{
			_storedItem.transform.parent = _happyStage.transform;
		}
		else if (_sadStage.activeInHierarchy)
		{
			_storedItem.transform.parent = _sadStage.transform;
		}

		RemoveItemFromInventory ();

	}

	private void Interact(GameObject trigger)
	{

		if(_happyStage.activeInHierarchy)
		{
			_storedItem.transform.parent = _happyStage.transform;
		}
		else if (_sadStage.activeInHierarchy)
		{
			_storedItem.transform.parent = _sadStage.transform;
		}

		int IDstored = _storedItem.GetComponent<StageObject> ().ID;
		int IDcollide = trigger.GetComponent<StageObject> ().ID;

		switch (IDcollide)
		{
		case (int)IDList.ID.Dummy:
			
			Dummy dummy = trigger.GetComponent<Dummy> ();
			if(IDstored == (int)IDList.ID.Mask)
			{
				_storedItem.SetActive (true);
				dummy.PutMask (_storedItem);
				changeObjectsStage ();

				RemoveItemFromInventory ();
			}
			break;

		case (int)IDList.ID.Mechanism:
			
			Mechanism mechanism = trigger.GetComponent<Mechanism> ();
			if (IDstored == (int)IDList.ID.Key)
			{
				bool keyUsed = mechanism.InsertKey ();
				if(keyUsed)
				{
					RemoveItemFromInventory ();
				}
			}
			else if (IDstored == (int)IDList.ID.Gear)
			{
				int gears = mechanism.InsertGear ();
				if(gears <= mechanism.MaxGears)
				{
					RemoveItemFromInventory ();
				}
			}
			break;

		case (int)IDList.ID.Box:

			Box box = trigger.GetComponent<Box> ();
			if (IDstored == (int)IDList.ID.Torch && box.isMetallic == false)
			{
				GameObject.Destroy (trigger);
			}
			break;
		}
	}

	private void changeObjectsStage ()
	{
		if (_happyStage.activeInHierarchy)
		{
			for(int i = 0; i < _pickups.Length; i++)
			{
				if(_pickups[i].GetComponent<StageObject> ().canSwapStage)
				{
					_pickups[i].transform.parent = _sadStage.transform;
				}
			}

			for(int i = 0; i < _interactive.Length; i++)
			{
				if(_interactive[i].GetComponent<StageObject> ().canSwapStage)
				{
					_interactive[i].transform.parent = _sadStage.transform;
				}
			}
		}
		else if (_sadStage.activeInHierarchy)
		{
			for(int i = 0; i < _pickups.Length; i++)
			{
				if(_pickups[i].GetComponent<StageObject> ().canSwapStage)
				{
					_pickups[i].transform.parent = _happyStage.transform;
				}
			}

			for(int i = 0; i < _interactive.Length; i++)
			{
				if(_interactive[i].GetComponent<StageObject> ().canSwapStage)
				{
					_interactive[i].transform.parent = _happyStage.transform;
				}
			}
		}
	}

	private void RemoveItemFromInventory ()
	{
		_storedItem = null;
		_pickedUp = false;
		InventoryIcon.enabled = false;
	}
}
