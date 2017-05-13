using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gate : StageObject 
{
	public void openGate()
	{
		SpriteRenderer color = this.gameObject.GetComponent<SpriteRenderer> ();
		color.color = new Color (255, 255, 255);
	}

	public void closeGate()
	{
		SpriteRenderer color = this.gameObject.GetComponent<SpriteRenderer> ();
		color.color = new Color (0, 0, 0);
	}
}
