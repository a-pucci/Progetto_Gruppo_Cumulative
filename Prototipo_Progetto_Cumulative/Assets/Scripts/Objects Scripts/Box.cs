using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : StageObject 
{
	[Header("-- BOX --")]
	public bool isMetallic;

	[Header("Drop List")]
	public GameObject[] Droppables;

	[Header("Drop Offset")]
	public float xOffset = 0f;
	public float yOffset = 0.8f;

	public AudioClip BurnClip;
	[Range(0.0f, 1.0f)] public float BurnVolume = 0.8f;
	public AudioClip LandingClip;
	[Range(0.0f, 1.0f)] public float LandingVolume = 0.8f;
	public AudioClip PushingClip;
	[Range(0.0f, 1.0f)] public float PushingVolume = 0.8f;
	private SFXController _sfxManager;

	private GameObject _happyStage;
	private GameObject _sadStage;

	void Awake()
	{
		base.ID = (int)IDList.ID.Box;
		_happyStage = GameObject.FindGameObjectWithTag ("HappyStage");
		_sadStage = GameObject.FindGameObjectWithTag ("SadStage");
		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
	}

	public void DropItems ()
	{
		float multiDropOffset = -1f;
		for (int i = 0; i < Droppables.Length; i++)
		{
			GameObject newItem = Instantiate (Droppables[i], new Vector3 (this.transform.position.x + xOffset + multiDropOffset, this.transform.position.y + yOffset, this.transform.position.z), this.transform.rotation);
			if(_happyStage.activeInHierarchy)
			{
				newItem.transform.parent = _happyStage.transform;
			}
			else if (_sadStage.activeInHierarchy)
			{
				newItem.transform.parent = _sadStage.transform;
			}
			multiDropOffset += 1f;
		}
	}

	public override bool CanInteract (GameObject other)
	{
		bool canInteract = false;
		if(other.GetComponent<StageObject> () != null)
		{
			int otherID = other.GetComponent<StageObject> ().ID;
			if(otherID == (int)IDList.ID.Torch && isMetallic == false)
			{
				canInteract = true;
			}		
		}
		return canInteract;
	}

	void OnCollisionEnter2D (Collision2D collider) {
		if (!collider.gameObject.CompareTag ("Player")) {
			_sfxManager.PlaySFX (LandingClip, LandingVolume);
		}
	}

	public override void Interact (ref GameObject other)
	{
		if(CanInteract (other))
		{
			_sfxManager.PlaySFX (BurnClip, BurnVolume);
			DropItems ();
			GameObject.Destroy (this.gameObject);
		}
	}
}
