using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gate : StageObject 
{
	private Collider2D _collider;

	void Start()
	{
		_collider = this.gameObject.transform.FindChild ("Collider").gameObject.GetComponent <Collider2D> ();
	}

	public void openGate()
	{
		SpriteRenderer color = this.gameObject.GetComponent<SpriteRenderer> ();
		color.color = new Color (255, 255, 255);
		_collider.enabled = false;
	}

	public void closeGate()
	{
		SpriteRenderer color = this.gameObject.GetComponent<SpriteRenderer> ();
		color.color = new Color (0, 0, 0);
		_collider.enabled = true;
	}
}
