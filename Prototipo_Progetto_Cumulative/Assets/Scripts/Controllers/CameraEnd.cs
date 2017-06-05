using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraEnd : MonoBehaviour 
{
	[Header("Next Level")]
	public string NextLevel;

	[Header("Camera Setting")]
	public float MovingSpeed;
	public float GrowingSpeed;
	public float MaxSize;
	public Animator CameraAC;

	[Header("UI Settings")]
	public GameObject[] Interface;
	public Animator LeftCurtainAC;
	public Animator RightCurtainAC;

	private float _closureTime = 2f;

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
			CameraAC.SetTrigger ("Grow");
			/*float sizeCovered = (Time.time - _startTime) * GrowingSpeed;
			float fraction = sizeCovered / _sizeDifference;

			_camera.orthographicSize = Mathf.Lerp (_initialSize, MaxSize, fraction);*/

			if(_camera.orthographicSize == MaxSize)
			{
				_growing = false;
				_isShowing = false;
				LeftCurtainAC.SetTrigger ("Close");
				RightCurtainAC.SetTrigger ("Close");
			}
		}
	}

	public void StartClose ()
	{
		for(int i = 0; i < Interface.Length; i++)
		{
			Interface[i].SetActive (false);
		}

		_moving = true;
		_isShowing = true;
		_closing = true;
		StartCoroutine (ChangeScene ());
	}

	public bool closingAnimation()
	{
		return _closing;
	}

	private IEnumerator ChangeScene()
	{

		while (_isShowing == true)
		{
			yield return new WaitForSeconds (0);
		}
		yield return new WaitForSeconds (_closureTime);
		SceneManager.LoadScene (NextLevel);
	}
}
