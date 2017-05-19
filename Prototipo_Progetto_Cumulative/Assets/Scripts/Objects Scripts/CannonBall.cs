using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : StageObject 
{
	public float time;
	public bool shooted;
	void OnTriggerEnter2D(Collider2D other)
	{	
		if(other.gameObject.GetComponent<StageObject> () != null && 
			(other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Box ||
			 other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Gate))
		{
			if(shooted)
			{
				GameObject.Destroy (other.gameObject, time);
				shooted = false;
			}
		}
	}
}
