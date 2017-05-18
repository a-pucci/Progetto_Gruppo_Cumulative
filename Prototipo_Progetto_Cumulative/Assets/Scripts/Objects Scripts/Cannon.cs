using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : StageObject 
{

	public GameObject shotStart;
	public GameObject projectile;
	public float BulletSpeed;
	public float RotationSpeed = 100;

	public bool controlled = false;

	[SerializeField ]private bool _hasBall;
	private PlayerMovement _player;
	private Vector2 _shootLocation;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement> ();
	}

	// Update is called once per frame
	void Update () 
	{
		_shootLocation = new Vector2(shotStart.transform.position.x, shotStart.transform.position.y);
		if (controlled == true) 
		{

			if (Input.GetKey (KeyCode.W)) 
			{
				transform.Rotate (Vector3.forward * RotationSpeed * Time.deltaTime);
			}

			if (Input.GetKey (KeyCode.S)) 
			{
				transform.Rotate (Vector3.back * RotationSpeed * Time.deltaTime);
			}

			if (Input.GetKeyDown (KeyCode.Q)) 
			{
				Launch ();
				controlled = false;
				_player.controllingCannon = false;
			}
		}
	}

	void Launch()
	{
		if(_hasBall)
		{
			GameObject clone = Instantiate (projectile, _shootLocation, Quaternion.identity) as GameObject;
			Rigidbody2D cloneRB = clone.GetComponent<Rigidbody2D> ();

			cloneRB.AddRelativeForce (transform.TransformDirection (new Vector2( (Mathf.Cos (transform.rotation.z * Mathf.Deg2Rad) * BulletSpeed),
				(Mathf.Sin (transform.rotation.z * Mathf.Deg2Rad) * BulletSpeed) )), ForceMode2D.Impulse);

			_hasBall = false;
		}
	}

	public bool InsertBall()
	{
		_hasBall = true;
		return _hasBall;
	}
	public bool hasBall()
	{
		return _hasBall;
	}
}
