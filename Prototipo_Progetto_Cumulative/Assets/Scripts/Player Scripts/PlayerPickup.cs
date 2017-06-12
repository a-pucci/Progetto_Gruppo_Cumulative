using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEditor;

public class PlayerPickup : MonoBehaviour 
{
	[Header("Inventory")]
	public GameObject ExclamationPoint;
	//public Text PickupText;
	//public Text InteractText;
	public Image InventoryIcon;

	[Header("Drop Offset")]
	public Vector3 TorchDropOffset;
	public Vector3 GearDropOffset;
	public Vector3 BallDropOffset;
	public Vector3 KeyDropOffset;

	[Header("Various Audio")]
	public AudioClip KeyPickupClip;
	public AudioClip PickupClip;
	public AudioClip DropClip;
	public AudioClip InteractClip;

	private GameObject _happyStage;
	private GameObject _sadStage;
	private GameObject _triggerObject;
	private GameObject _storedItem;
	private PlayerMovement _player;

	private SFXController _sfxManager;

	private bool _pickedUp = false;

	void Awake () 
	{
		_happyStage = GameObject.FindGameObjectWithTag ("HappyStage");
		_sadStage = GameObject.FindGameObjectWithTag ("SadStage");
		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
		_player = GetComponent<PlayerMovement> ();

		ExclamationPoint.SetActive (false);
		//PickupText.enabled = false;
		//InteractText.enabled = false;
		InventoryIcon.enabled = false;
	}

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

//		if (collision.CompareTag("Pickup") && InteractText.enabled == false && _storedItem == null)
//		{
//			PickupText.enabled = true;	
//		}
//
//		else if (collision.CompareTag("Interactive") && PickupText.enabled == false && _storedItem != null)
//		{
//			InteractText.enabled = true;
//		}

		if (collision.CompareTag("Pickup"))
		{
			ExclamationPoint.SetActive (true);
		}
		else if (collision.CompareTag("Interactive") && _storedItem != null && _triggerObject.GetComponent<StageObject> ().CanInteract(_storedItem))
		{
			ExclamationPoint.SetActive (true);
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		_triggerObject = null;
		ExclamationPoint.SetActive (false);
		//PickupText.enabled = false;
		//InteractText.enabled = false;
	}

	private void Pickup()
	{
		_pickedUp = true;
		_storedItem = _triggerObject.GetComponent <StageObject> ().Pickup ();
		if(_storedItem.GetComponent <StageObject> ().ID == (int)IDList.ID.Key)
		{
			_sfxManager.PlaySFX (KeyPickupClip);
		}
		else
		{
			_sfxManager.PlaySFX (PickupClip);
		}

		InventoryIcon.enabled = true;
		InventoryIcon.sprite =  _storedItem.GetComponent<SpriteRenderer> ().sprite;
		_storedItem.GetComponent<SpriteRenderer> ().enabled = true;

		_storedItem.transform.parent = this.transform.parent;
		_storedItem.SetActive (false);
	}

	private void Drop ()
	{
		if(_player.isGrounded ())
		{
			_storedItem.SetActive (true);
			_sfxManager.PlaySFX (DropClip);

			switch (_storedItem.GetComponent<StageObject> ().ID)
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

			if (_happyStage.activeInHierarchy)
			{
				_storedItem.transform.parent = _happyStage.transform;
			}
			else if (_sadStage.activeInHierarchy)
			{
				_storedItem.transform.parent = _sadStage.transform;
			}

			RemoveItemFromInventory ();
		}
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

		if(trigger.GetComponent<StageObject> ().CanInteract (_storedItem))
		{
			trigger.GetComponent<StageObject> ().Interact (ref _storedItem);
		}
	}

	public void RemoveItemFromInventory ()
	{
		_storedItem = null;
		_pickedUp = false;
		InventoryIcon.enabled = false;
	}
}

