using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{
	[Header("Movement")]
	public float Speed = 5.0f;
	public bool SmoothMovement;

	[Header("Position")]
	public Transform EnemyTransform;
	public GameObject EndPosition;

	private Vector3 _initialPosition;
	private Vector3 _destination;
	private float _startTime;
	private float _journeyLenght;


	// Use this for initialization
	void Start () 
	{
		_initialPosition = EnemyTransform.position;
		_destination = EndPosition.transform.position;
		_startTime = Time.time;
		_journeyLenght = Vector3.Distance (_initialPosition, _destination);

		EndPosition.SetActive (false);


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

			EnemyTransform.position = Vector3.Lerp (_initialPosition, _destination, fractionJourney);

			if(EnemyTransform.position == _destination)
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
		if(this.GetComponentInChildren<Key>() != null)
		{
			return true;
		}

		return false;
	}
}
