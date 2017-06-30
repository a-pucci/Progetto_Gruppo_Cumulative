using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CannonBall : StageObject 
{
	public float time;
	public bool shooted;
	public AudioClip boxDestroyed;
	[Range(0.0f, 1.0f)] public float boxDestroyedVolume = 0.8f;
	public AudioClip metalBoxDestroyed;
	[Range(0.0f, 1.0f)] public float metalBoxDestroyedVolume = 0.8f;
	public AudioClip gateDestroyed;
	[Range(0.0f, 1.0f)] public float gateDestroyedVolume = 0.8f;
	private SFXController _sfxManager;

	void Start()
	{
		base.ID = (int)IDList.ID.CannonBall;
		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{	
		if(other.gameObject.GetComponent<StageObject> () != null && 
			(other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Box ||
			 other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Gate))
		{
			if(shooted)
			{
				if (other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Box) 
				{
					if (other.gameObject.GetComponent<Box> ().isMetallic) {
						_sfxManager.PlaySFX (metalBoxDestroyed, metalBoxDestroyedVolume);
					} else {
						_sfxManager.PlaySFX (boxDestroyed, boxDestroyedVolume);
					}
					other.gameObject.GetComponent<Box> ().DropItems ();
				} 
				else if ( other.gameObject.GetComponent<StageObject> ().ID == (int)IDList.ID.Gate) 
				{
					_sfxManager.PlaySFX (gateDestroyed, gateDestroyedVolume);
				}

				GameObject.Destroy (other.gameObject, time);
				shooted = false;
			}
		}
	}

	public override GameObject Pickup ()
	{
		return this.gameObject;
	}

	public override bool CanInteract (GameObject other)
	{
		bool canInteract = false;
		if(other.GetComponent<StageObject> () != null)
		{
			int otherID = other.GetComponent<StageObject> ().ID;
			if(otherID == (int)IDList.ID.Cannon)
			{
				canInteract = true;
			}		
		}
		return canInteract;
	}
}
