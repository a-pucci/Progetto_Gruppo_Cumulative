using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnd : MonoBehaviour 
{
	public float MovingSpeed;
	public float GrowingSpeed;
	public float MaxSize;
	public Canvas Interface;

	private Camera _camera;
	private Transform _cameraTransform;
	private Vector3 _center;

	private float _initialSize;
	private float _startTime;
	private float _sizeDifference;

	private bool _moving = false;
	private bool _growing = false;
	private bool _isShowing = false;
	private bool _closing = false;

	// Use this for initialization
	void Start () 
	{
		_camera = Camera.main;
		_cameraTransform = this.gameObject.transform;
		_center = new Vector3 (0, 0, -10);

		_initialSize = _camera.orthographicSize;
		_startTime = Time.time;
		_sizeDifference = MaxSize - _initialSize;
	}

	void Update()
	{
		if(_moving)
		{
			_cameraTransform.position = Vector3.MoveTowards (_cameraTransform.position, _center, MovingSpeed * Time.deltaTime);
			if(_cameraTransform.position == _center)
			{
				_moving = false;
				_growing = true;
			}
		}
		if(_growing)
		{
			float sizeCovered = (Time.time - _startTime) * GrowingSpeed;
			float fraction = sizeCovered / _sizeDifference;

			_camera.orthographicSize = Mathf.Lerp (_initialSize, MaxSize, fraction);

			if(_camera.orthographicSize == MaxSize)
			{
				_growing = false;
				_isShowing = false;
			}
		}
	}

	public void StartClose ()
	{
		Interface.enabled = false;
		_moving = true;
		_isShowing = true;
		_closing = true;
	}

	public bool IsShowing()
	{
		return _isShowing;
	}

	public bool closingAnimation()
	{
		return _closing;
	}
}
