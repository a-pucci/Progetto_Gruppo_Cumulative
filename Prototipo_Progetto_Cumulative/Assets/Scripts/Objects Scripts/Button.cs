using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : StageObject
{
	public GameObject[] platformToMove;
	public GameObject[] gateToOpen;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player") || 
			other.gameObject.CompareTag("Enemy") ||
			other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Box)
		{
			for(int i = 0; i < platformToMove.Length; i++)
			{
				platformToMove [i].GetComponent<PlatformMovement> ().StartMove ();
			}

			for(int i = 0; i < gateToOpen.Length; i++)
			{
				gateToOpen [i].GetComponent<Gate> ().openGate();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		for(int i = 0; i < platformToMove.Length; i++)
		{
			platformToMove [i].GetComponent<PlatformMovement> ().StopMove ();
		}

		for(int i = 0; i < gateToOpen.Length; i++)
		{
			gateToOpen [i].GetComponent<Gate> ().closeGate();
		}
	}

}
