using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEditor;

public class PlayerPickup : MonoBehaviour 
{
	[Header("Inventory")]
	public Text PickupText;
	public Text InteractText;
	public Image InventoryIcon;

	[Header("Drop Offset")]
	public Vector3 TorchDropOffset;
	public Vector3 GearDropOffset;
	public Vector3 BallDropOffset;
	public Vector3 KeyDropOffset;

	[Header("Various Audio")]
//	public AudioClip KeyPickupClip;
//	public AudioClip PickupClip;
	public AudioClip DropClip;
	public AudioClip InteractClip;

	private GameObject _happyStage;
	private GameObject _sadStage;

	private GameObject _triggerObject;
	private GameObject _storedItem;


	private SFXController _sfxManager;

	private bool _pickedUp = false;


	// Use this for initialization
	void Awake () 
	{

		_happyStage = GameObject.FindGameObjectWithTag ("HappyStage");
		_sadStage = GameObject.FindGameObjectWithTag ("SadStage");

		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
		
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

		if (collision.CompareTag("Pickup") && InteractText.enabled == false && _storedItem == null)
		{
			PickupText.enabled = true;	
		}
		else if (collision.CompareTag("Interactive") && PickupText.enabled == false && _storedItem != null)
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
		_storedItem = _triggerObject;
//		_sfxManager.PlaySFX (PickupClip);

		InventoryIcon.enabled = true;
		InventoryIcon.sprite =  _storedItem.GetComponent<SpriteRenderer> ().sprite;
		_triggerObject.GetComponent<SpriteRenderer> ().enabled = true;


		_storedItem.transform.parent = this.transform.parent;
		_storedItem.SetActive (false);
	}

	private void Drop ()
	{
		_storedItem.SetActive (true);

		_sfxManager.PlaySFX (DropClip);

		switch(_storedItem.GetComponent<StageObject> ().ID)
		{
		case (int)IDList.ID.CannonBall:
			_storedItem.transform.position = this.transform.position + BallDropOffset;
			break;

		case (int)IDList.ID.Gear:
			_storedItem.transform.position = this.transform.position + GearDropOffset;
			break;

		case (int)IDList.ID.Key:
			_storedItem.transform.position = this.transform.position + KeyDropOffset;
			break;

		case (int)IDList.ID.Torch:
			_storedItem.transform.position = this.transform.position + TorchDropOffset;
			break;

		}

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

//		int IDstored = _storedItem.GetComponent<StageObject> ().ID;
//		int IDcollide = trigger.GetComponent<StageObject> ().ID;


		if(_storedItem.GetComponent<StageObject> ().CanInteract (trigger))
		{
			trigger.GetComponent<StageObject> ().Interact (_storedItem);

			if(_storedItem.GetComponent<StageObject> ().IsDestroyedOnUse ())
			{
				RemoveItemFromInventory ();
			}
		}

//		switch (IDcollide)
//		{
//		case (int)IDList.ID.Dummy:
//			
//			Dummy dummy = trigger.GetComponent<Dummy> ();
//			if(IDstored == (int)IDList.ID.Mask)
//			{
//				_storedItem.SetActive (true);
//				dummy.PutMask (_storedItem);
//				RemoveItemFromInventory ();
//			}
//			break;
//
//		case (int)IDList.ID.Mechanism:
//			
//			Mechanism mechanism = trigger.GetComponent<Mechanism> ();
//			if (IDstored == (int)IDList.ID.Key)
//			{
//				bool keyUsed = mechanism.InsertKey ();
//				if(keyUsed)
//				{
//					RemoveItemFromInventory ();
//				}
//			}
//			else if (IDstored == (int)IDList.ID.Gear)
//			{
//				_sfxManager.PlaySFX (InsertMechanismClip);
//				int gears = mechanism.InsertGear ();
//				if(gears <= mechanism.GetMaxGears())
//				{
//					RemoveItemFromInventory ();
//				}
//			}
//			break;
//
//		case (int)IDList.ID.Box:
//
//			Box box = trigger.GetComponent<Box> ();
//			if (IDstored == (int)IDList.ID.Torch && box.isMetallic == false)
//			{
//				box.DropItems ();
//				GameObject.Destroy (trigger);
//			}
//			break;
//
//		case (int)IDList.ID.Cannon:
//			
//			Cannon cannon = trigger.GetComponent<Cannon> ();
//			if(IDstored == (int)IDList.ID.Torch && cannon.hasBall())
//			{
//				cannon.setControl (true);
//				this.gameObject.GetComponent<PlayerUserController> ().CanMove = false;
//			}
//			else if (IDstored == (int)IDList.ID.CannonBall)
//			{
//				bool loaded = cannon.InsertBall ();
//				if (loaded)
//				{
//					RemoveItemFromInventory ();
//				}
//			}
//			break;
//		}
	}

	private void RemoveItemFromInventory ()
	{
		_storedItem = null;
		_pickedUp = false;
		InventoryIcon.enabled = false;
	}
}
