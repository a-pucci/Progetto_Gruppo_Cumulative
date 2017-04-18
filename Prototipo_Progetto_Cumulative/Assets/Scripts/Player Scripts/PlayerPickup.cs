using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerPickup : MonoBehaviour 
{
	public Text PickupText;
	public Image HappyMaskImage;
	public GameObject HappyMask;

	private GameObject _triggerObject;
	private bool _pickedUp = false;


	// Use this for initialization
	void Start () 
	{
		PickupText.enabled = false;
		HappyMaskImage.enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if(_triggerObject != null && CrossPlatformInputManager.GetButtonDown("Pickup"))
		{
			if (_triggerObject.CompareTag ("Pickup"))
			{
				_pickedUp = true;
				PickupText.enabled = false;
				HappyMaskImage.enabled = true;
				_triggerObject.gameObject.SetActive (false);
			}
			else if (_triggerObject.CompareTag("Interactive")  && _pickedUp)
			{
				_pickedUp = false;
				HappyMaskImage.enabled = false;
				Instantiate (HappyMask, new Vector3(_triggerObject.transform.position.x + 0.4f, _triggerObject.transform.position.y + 2f, _triggerObject.transform.position.z), _triggerObject.transform.rotation);
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
}
