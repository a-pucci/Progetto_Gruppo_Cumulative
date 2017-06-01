using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour 
{
	[Header("Movement")]
	public float speed = 1.0f;

	private Transform _platformTransform;
	private GameObject _endPosition;

	private Vector3 _initialPosition;
	private Vector3 _destination;

	private GameObject _player;
	private Transform _playerParent;

	[SerializeField]private bool _canMove = false;


	// Use this for initialization
	void Start () 
	{
		_platformTransform = this.gameObject.transform;
		_endPosition = this.gameObject.transform.parent.FindChild ("EndPosition").gameObject;

		_player = GameObject.FindGameObjectWithTag ("Player");
		_playerParent = _player.transform.parent;

		_initialPosition = _platformTransform.position;
		_destination = _endPosition.transform.position;

		_endPosition.SetActive (false);
	}

	// Update is called once per frame
	void Update () 
	{
		if(_canMove)
		{

			_platformTransform.position = Vector3.MoveTowards (_platformTransform.position, _destination, speed * Time.deltaTime);

			if(_platformTransform.position == _destination)
			{
				Vector3 _temp = _initialPosition;
				_initialPosition = _destination;
				_destination = _temp;
			}
		}			
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.transform.parent = this.gameObject.transform;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.transform.parent = null;
		}
	}

	public void StartMove()
	{
		_canMove = true;
	}

	public void StopMove()
	{
		_canMove = false;
	}
}
