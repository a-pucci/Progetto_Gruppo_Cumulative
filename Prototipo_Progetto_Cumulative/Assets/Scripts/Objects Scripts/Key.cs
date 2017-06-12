using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : StageObject
{
	public GameObject KeyPrefab;
	public EnemyMovement Enemy;
	private PlayerPickup _player;
	public bool EnemyChild;

	void Start()
	{
		base.ID = (int)IDList.ID.Key;
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerPickup> ();
	}

	public override GameObject Pickup ()
	{
		if(EnemyChild)
		{
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			this.gameObject.tag = "Interactive";
			Enemy.EnableMovement (false);
			GameObject newKey = Instantiate (KeyPrefab);
			newKey.SetActive (false);

			return newKey;
		}
		else
		{
			return this.gameObject;
		}


	}

	public override void Interact (ref GameObject other)
	{
		if(CanInteract(other))
		{
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			this.gameObject.tag = "Pickup";
			Enemy.EnableMovement (true);
			_player.RemoveItemFromInventory ();
		}
	}

	public override bool CanInteract (GameObject other)
	{
		bool canInteract = false;
		if(other.GetComponent<StageObject> () != null)
		{
			int otherID = other.GetComponent<StageObject> ().ID;
			if(otherID == (int)IDList.ID.Mechanism || otherID == (int)IDList.ID.Key)
			{
				canInteract = true;
			}		
		}
		return canInteract;
	}
}
