using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour 
{
	[Header("Movement")]
	public float speed = 1.0f;

	[Header("Position")]
	public Transform PlatformTransform;
	public GameObject EndPosition;

	private Vector3 _initialPosition;
	private Vector3 _destination;

	[SerializeField]private bool _canMove = false;


	// Use this for initialization
	void Start () 
	{
		_initialPosition = PlatformTransform.position;
		_destination = EndPosition.transform.position;

		EndPosition.SetActive (false);
	}

	// Update is called once per frame
	void Update () 
	{
		if(_canMove)
		{

			PlatformTransform.position = Vector3.MoveTowards (PlatformTransform.position, _destination, speed * Time.deltaTime);

			if(PlatformTransform.position == _destination)
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
