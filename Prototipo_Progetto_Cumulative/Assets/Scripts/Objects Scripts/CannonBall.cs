using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : StageObject 
{
	void OnTriggerEnter2D(Collider2D other)
	{	
		if(other.gameObject.GetComponent<StageObject> () != null && 
			(other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Box ||
			 other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Gate))
			{
				GameObject.Destroy (other.gameObject);
			}
	}
}
