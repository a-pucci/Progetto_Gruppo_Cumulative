using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{
	[Header("Movement")]
	public float Speed = 5.0f;
	public bool SmoothMovement;

	private Transform _enemyTransform;
	private GameObject _endPosition;

	private Vector3 _initialPosition;
	private Vector3 _destination;
	private float _startTime;
	private float _journeyLenght;


	// Use this for initialization
	void Start () 
	{
		_enemyTransform = this.gameObject.transform;
		_endPosition = this.gameObject.transform.parent.FindChild ("EndPosition").gameObject;
		
		_initialPosition = _enemyTransform.position;
		_destination = _endPosition.transform.position;
		_startTime = Time.time;
		_journeyLenght = Vector3.Distance (_initialPosition, _destination);

		_endPosition.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(CanMove ())
		{
			float distanceCovered = (Time.time - _startTime) * Speed;
			float fractionJourney = distanceCovered / _journeyLenght;
			if(SmoothMovement)
			{
				fractionJourney = Mathf.SmoothStep (0f, 1f, fractionJourney);
			}

			_enemyTransform.position = Vector3.Lerp (_initialPosition, _destination, fractionJourney);

			if(_enemyTransform.position == _destination)
			{
				Vector3 _temp = _initialPosition;
				_initialPosition = _destination;
				_destination = _temp;

				_startTime = Time.time;

				Vector3 newScale = transform.localScale;
				newScale.x *= -1;
				transform.localScale = newScale;
			}
		}			
	}

	public bool CanMove()
	{
		return (GetComponentInChildren<Key> () != null);
	}
}
