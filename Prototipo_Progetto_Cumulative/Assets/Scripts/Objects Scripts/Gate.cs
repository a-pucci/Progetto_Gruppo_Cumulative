using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gate : StageObject 
{
//	public GameObject[] dummies;
	public string newScene;

	private bool _gateOpen = false;

//	public void checkOpen()
//	{
//		_gateOpen = true;
//		for(int i = 0; i < dummies.Length; i++)
//		{
//			Dummy dummy = dummies [i].GetComponent<Dummy> ();
//			if(!dummy.HasMask ())
//			{
//				_gateOpen = false;
//			}
//		}
//		if (_gateOpen)
//		{
//			openGate ();
//		}
//		else
//		{
//			closeGate ();
//		}
//	}

	public void openGate()
	{
		_gateOpen = true;
		SpriteRenderer color = this.gameObject.GetComponent<SpriteRenderer> ();
		color.color = new Color (255, 255, 255);
	}

	public void closeGate()
	{
		_gateOpen = false;
		SpriteRenderer color = this.gameObject.GetComponent<SpriteRenderer> ();
		color.color = new Color (0, 0, 0);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.CompareTag ("Player") && _gateOpen == true)
		{
			SceneManager.LoadScene (newScene);
		}			
	}
}
