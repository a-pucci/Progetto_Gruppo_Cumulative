using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gate : StageObject 
{
	private GameObject _collider;

	void Start()
	{
		_collider = this.gameObject.transform.FindChild ("Collider").gameObject;
	}

	public void openGate()
	{
		SpriteRenderer color = this.gameObject.GetComponent<SpriteRenderer> ();
		color.color = new Color (255, 255, 255);
		_collider.SetActive (false);
	}

	public void closeGate()
	{
		SpriteRenderer color = this.gameObject.GetComponent<SpriteRenderer> ();
		color.color = new Color (0, 0, 0);
		_collider.SetActive (true);
	}
}
