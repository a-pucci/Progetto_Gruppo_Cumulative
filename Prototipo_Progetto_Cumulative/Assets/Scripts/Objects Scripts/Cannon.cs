﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Cannon : StageObject 
{
	public GameObject Bullet;
	public float BulletForce;
	public float RotationSpeed = 100;
	public float CameraWaitAfterShoot;

	private bool _controlled = false;
	private bool _hasBall;
	private PlayerUserController _player;
	private Camera2DFollow _camera;
	private Vector2 _shootLocation;
	private Transform _shotStart;
	private GameObject _aim;
	private PlayerPickup _playerPick;
	private StageSwapController _stageSwap;
	private Collider2D _collider;
	private RaycastHit2D _hit;
	private Animator _playerAC;
	private SFXController _sfxManager;

	[Header("Cannon Audio")]
	public AudioClip CannonClip;
	[Range(0.0f, 1.0f)] public float CannonVolume = 0.8f;

	// Use this for initialization
	void Start () 
	{
		base.ID = (int)IDList.ID.Cannon;
		_stageSwap = GameObject.FindGameObjectWithTag ("StageSwap").GetComponent<StageSwapController> ();
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera2DFollow> ();
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerUserController> ();
		_playerPick = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerPickup> ();
		_playerAC = GameObject.FindGameObjectWithTag ("Player").transform.FindChild("Sprite").GetComponent <Animator> ();
		_shotStart = this.gameObject.transform.FindChild ("BarrelEnd");
		_collider = this.gameObject.GetComponent<Collider2D> ();
		_aim = this.gameObject.transform.FindChild ("Aim").gameObject;
		_aim.SetActive (false);
		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (_controlled == true) 
		{
			_shootLocation = new Vector2(_shotStart.position.x, _shotStart.position.y);

			_hit = Physics2D.Raycast (_shootLocation, transform.right);
			_aim.transform.position = _hit.point;

			if (CrossPlatformInputManager.GetButton("CannonUp")) 
			{
				if(transform.rotation.z < 0.5)
				{
					transform.Rotate (Vector3.forward * RotationSpeed * Time.deltaTime);
				}
			}

			if (CrossPlatformInputManager.GetAxis ("Cannon") > 0.2 || CrossPlatformInputManager.GetAxis ("Cannon") < -0.2) {
				Vector3 rotation = new Vector3 (0, 0, 0);
				Debug.Log (CrossPlatformInputManager.GetAxis ("Cannon"));
				if (CrossPlatformInputManager.GetAxis ("Cannon") > 0.2) {
					rotation = Vector3.back;
				} else if (CrossPlatformInputManager.GetAxis ("Cannon") < -0.2) {
					rotation = Vector3.forward;
				}
				if(transform.rotation.z < 0.5 || transform.rotation.z > 0)
				{
					transform.Rotate (rotation * RotationSpeed * Time.deltaTime);
				}
			}

			if (CrossPlatformInputManager.GetButton("CannonDown")) 
			{
				if(transform.rotation.z > 0)
				{
					transform.Rotate (Vector3.back * RotationSpeed * Time.deltaTime);
				}
			}
				

			if (CrossPlatformInputManager.GetButtonDown("CannonFire")) 
			{
				Shoot ();
				_collider.enabled = true;
				_controlled = false;
				_aim.SetActive (false);
				_player.CanMove = true;
				_camera.LockCamera (false, CameraWaitAfterShoot);
				_stageSwap.UnlockSwap ();
			}
		}
	}

	void Shoot()
	{
		if(_hasBall)
		{
			_sfxManager.PlaySFX (CannonClip, CannonVolume);
			GameObject clone = Instantiate (Bullet, _shootLocation, Quaternion.identity) as GameObject;
			Rigidbody2D cloneRB = clone.GetComponent<Rigidbody2D> ();
			SpriteRenderer cloneSR = clone.GetComponent <SpriteRenderer> ();
			cloneSR.sortingLayerName = "Interactive";
			clone.transform.parent = this.gameObject.transform.parent;
			clone.GetComponent<CannonBall> ().shooted = true;

			cloneRB.AddRelativeForce (transform.TransformDirection (new Vector2( (Mathf.Cos (transform.rotation.z * Mathf.Deg2Rad) * BulletForce),
				(Mathf.Sin (transform.rotation.z * Mathf.Deg2Rad) * BulletForce) )), ForceMode2D.Impulse);

			_hasBall = false;
		}
	}

	public override void Interact (ref GameObject other)
	{
		if(CanInteract (other))
		{
			if(other.GetComponent<StageObject>().ID == (int)IDList.ID.Torch && _hasBall)
			{
				_collider.enabled = false;
				_controlled = true;
				_player.CanMove = false;
				_aim.SetActive (true);
				_camera.LockCamera (true);
				_stageSwap.LockSwap ();
				_playerAC.SetFloat("Speed", 0f);
			}
			else if(other.GetComponent<StageObject>().ID == (int)IDList.ID.CannonBall)
			{
				_hasBall = true;
				_playerPick.RemoveItemFromInventory ();
			}
		}
	}

	public override bool CanInteract (GameObject other)
	{
		bool canInteract = false;
		if(other.GetComponent<StageObject> () != null)
		{
			int otherID = other.GetComponent<StageObject> ().ID;
			if(otherID == (int)IDList.ID.CannonBall || (otherID == (int)IDList.ID.Torch && _hasBall))
			{
				canInteract = true;
			}		
		}
		return canInteract;
	}
} 
	