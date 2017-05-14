using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerPickup : MonoBehaviour 
{
	public Text PickupText;
	public Image InventoryIcon;
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
					DropItem ();
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
			DropItem ();
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		_triggerObject = collision.gameObject;
		if(collision.CompareTag("Interactive") || collision.CompareTag("Pickup"))
		{
			PickupText.enabled = true;	
		}
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

		if(_storedItem.transform.parent != null && _storedItem.transform.parent.tag == "Enemy")
		{
			GameObject enemyParent = new GameObject("enemyParent");

			enemyParent = _storedItem.transform.parent.gameObject;
			_storedItem.transform.parent = this.transform.parent;
			Destroy (enemyParent);
		}
		else
		{
			_storedItem.transform.parent = this.transform.parent;
		}

		_storedItem.SetActive (false);
	}

	private void DropItem ()
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

		_storedItem = null;
		_pickedUp = false;
		InventoryIcon.enabled = false;

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
				_storedItem = null;
				_pickedUp = false;
				InventoryIcon.enabled = false;
			}
			break;

		case (int)IDList.ID.Mechanism:
			
			Mechanism mechanism = trigger.GetComponent<Mechanism> ();
			if (IDstored == (int)IDList.ID.Key)
			{
				bool keyUsed = mechanism.InsertKey ();
				if(keyUsed)
				{
					_storedItem = null;
					_pickedUp = false;
					InventoryIcon.enabled = false;
				}
			}
			else if (IDstored == (int)IDList.ID.Gear)
			{
				int gears = mechanism.InsertGear ();
				if(gears <= mechanism.MaxGears)
				{
					_storedItem = null;
					_pickedUp = false;
					InventoryIcon.enabled = false;
				}
			}
			break;

		case (int)IDList.ID.Box:
			
			if (IDstored == (int)IDList.ID.Torch)
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
}
