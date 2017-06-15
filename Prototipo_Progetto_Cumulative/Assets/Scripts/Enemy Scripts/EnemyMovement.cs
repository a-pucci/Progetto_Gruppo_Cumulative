using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{
	[Header("Movement")]
	public float Speed = 0.7f;
	public bool SmoothMovement;
	[SerializeField] private bool _canMove;

	private Transform _enemyTransform;
	private GameObject _endPosition;

	private Vector3 _initialPosition;
	private Vector3 _destination;

	// Use this for initialization
	void Start () 
	{
		_enemyTransform = this.gameObject.transform;
		_endPosition = this.gameObject.transform.parent.FindChild ("EndPosition").gameObject;
		
		_initialPosition = _enemyTransform.position;
		_destination = _endPosition.transform.position;

		_endPosition.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_canMove)
		{

			_enemyTransform.position = Vector3.MoveTowards (_enemyTransform.position, _destination, Speed * Time.deltaTime);

			if(_enemyTransform.position == _destination)
			{
				Vector3 _temp = _initialPosition;
				_initialPosition = _destination;
				_destination = _temp;

				Vector3 newScale = transform.localScale;
				newScale.x *= -1;
				transform.localScale = newScale;
			}
		}			
	}

	public bool CanMove()
	{
		return _canMove;
	}

	public void EnableMovement(bool value)
	{
		_canMove = value;
	}
}
