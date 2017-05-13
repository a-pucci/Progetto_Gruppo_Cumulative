using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour 
{

	public float speed = 1.0f;

	public Transform _platformTransform;
	public GameObject endPosition;

	private Vector3 _initialPosition;
	private Vector3 _destination;

	[SerializeField]private bool _canMove = false;


	// Use this for initialization
	void Start () 
	{
		_initialPosition = _platformTransform.position;
		_destination = endPosition.transform.position;

		endPosition.SetActive (false);
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

	public void StartMove()
	{
		_canMove = true;
	}

	public void StopMove()
	{
		_canMove = false;
	}
}
