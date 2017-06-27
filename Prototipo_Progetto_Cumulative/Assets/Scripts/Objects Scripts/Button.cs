using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : StageObject
{
	[Header("-- BUTTON --")]

	[Header("Sprites")]
	public Sprite ButtonUpSprite;
	public Sprite ButtonDownSprite;

	[Header("Objects Connected")]
	public GameObject[] PlatformToMove;
	public GameObject[] GateToOpen;

	private List<Collider2D> _triggers;

	void Start()
	{
		base.ID = (int)IDList.ID.Button;
		_triggers = new List<Collider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (CanActivate (other))
		{
			GetComponentInChildren<SpriteRenderer> ().sprite = ButtonDownSprite;
			transform.FindChild ("Sprite").transform.localPosition = new Vector3 (0f, -1f, 0f);

			_triggers.Add (other);

			for (int i = 0; i < PlatformToMove.Length; i++)
			{
				PlatformToMove [i].GetComponent<PlatformMovement> ().StartMove ();
			}

			for (int i = 0; i < GateToOpen.Length; i++)
			{
				GateToOpen [i].GetComponent<Gate> ().openGate ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{	
		_triggers.Remove (other);

		if(_triggers.Count == 0)
		{
			GetComponentInChildren<SpriteRenderer> ().sprite = ButtonUpSprite;
			transform.FindChild ("Sprite").transform.localPosition = new Vector3 (0f, 0f, 0f);

			for (int i = 0; i < PlatformToMove.Length; i++)
			{
				PlatformToMove [i].GetComponent<PlatformMovement> ().StopMove ();
			}

			for (int i = 0; i < GateToOpen.Length; i++)
			{
				GateToOpen [i].GetComponent<Gate> ().closeGate ();
			}
		}
	}

	private bool CanActivate(Collider2D trigger)
	{
		if(trigger.gameObject.CompareTag ("Player") ||
			trigger.gameObject.CompareTag ("Enemy") ||
			(trigger.gameObject.GetComponent<StageObject> () != null &&
				trigger.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Box))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
