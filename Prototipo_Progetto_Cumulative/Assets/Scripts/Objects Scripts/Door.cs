using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour 
{
	public GameObject[] dummies;
	public string newScene;

	private bool _doorOpen = false;

	public void checkOpen()
	{
		_doorOpen = true;
		for(int i = 0; i < dummies.Length; i++)
		{
			Dummy dummy = dummies [i].GetComponent<Dummy> ();
			if(!dummy.HasMask ())
			{
				_doorOpen = false;
			}
		}
		if (_doorOpen)
		{
			openDoor ();
		}
		else
		{
			closeDoor ();
		}
	}

	private void openDoor()
	{
		SpriteRenderer color = this.gameObject.GetComponent<SpriteRenderer> ();
		color.color = new Color (255, 255, 255);
	}

	private void closeDoor()
	{
		SpriteRenderer color = this.gameObject.GetComponent<SpriteRenderer> ();
		color.color = new Color (113, 68, 0);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.CompareTag ("Player") && _doorOpen == true)
		{
			SceneManager.LoadScene (newScene);
		}			
	}
}
