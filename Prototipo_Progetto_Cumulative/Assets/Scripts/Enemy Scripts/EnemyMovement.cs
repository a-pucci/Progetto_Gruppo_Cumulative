using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public float speed = 5.0f;

	public Transform _enemyTransform;
	public GameObject endPosition;

	public bool smoothMovement;
	public bool canMove = true;

	private Vector3 _initialPosition;
	private Vector3 _destination;
	private float _startTime;
	private float _journeyLenght;


	// Use this for initialization
	void Start () 
	{
		_initialPosition = _enemyTransform.position;
		_destination = endPosition.transform.position;
		_startTime = Time.time;
		_journeyLenght = Vector3.Distance (_initialPosition, _destination);

		endPosition.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(canMove)
		{
			float distanceCovered = (Time.time - _startTime) * speed;
			float fractionJourney = distanceCovered / _journeyLenght;
			if(smoothMovement)
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



}
