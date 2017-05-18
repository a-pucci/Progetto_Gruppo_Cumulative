using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : StageObject
{
	[Header("-- BUTTON --")]
	[Header("Objects Connected")]
	public GameObject[] PlatformToMove;
	public GameObject[] GateToOpen;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player") || 
			other.gameObject.CompareTag("Enemy") ||
			(other.gameObject.GetComponent<StageObject> () != null &&
			other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Box))
		{
			for(int i = 0; i < PlatformToMove.Length; i++)
			{
				PlatformToMove [i].GetComponent<PlatformMovement> ().StartMove ();
			}

			for(int i = 0; i < GateToOpen.Length; i++)
			{
				GateToOpen [i].GetComponent<Gate> ().openGate();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		for(int i = 0; i < PlatformToMove.Length; i++)
		{
			PlatformToMove [i].GetComponent<PlatformMovement> ().StopMove ();
		}

		for(int i = 0; i < GateToOpen.Length; i++)
		{
			GateToOpen [i].GetComponent<Gate> ().closeGate();
		}
	}

}
