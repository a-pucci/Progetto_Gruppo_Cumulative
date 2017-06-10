using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
	[Header("Target")]
    public float damping = 0;
    public float lookAheadFactor = 0;
    public float lookAheadReturnSpeed = 0f;
    public float lookAheadMoveThreshold = 0f;
	public Vector3 offset;

	[Header("Camera Bounds")]
	public float XBound;
	public float YBound;

	[Header("Camera Animator")]
	public Animator CameraAC;

    private float _offsetZ;
    private Vector3 _lastTargetPosition;
    private Vector3 _currentVelocity;
    private Vector3 _lookAheadPos;

	private Transform _target;

	private HealthManager _playerHealth;
	private CameraStart _cameraStart;
	private CameraEnd _cameraEnd;

	private bool _locked;

    // Use this for initialization
    void Start()
    {
		_target = GameObject.FindGameObjectWithTag ("Player").transform;
		_lastTargetPosition = _target.position;
		_offsetZ = (transform.position - _target.position).z;
        transform.parent = null;
		_playerHealth = _target.gameObject.GetComponent<HealthManager> ();
		_cameraStart = GetComponent <CameraStart> ();
		_cameraEnd = GetComponent <CameraEnd> ();
	}

    // Update is called once per frame
    void Update()
	{
		if (!_playerHealth.PlayerDead && !_cameraStart.IsShowing() && !_cameraEnd.closingAnimation() && !_locked)
		{
			// only update lookahead pos if accelerating or changed direction
			float xMoveDelta = (_target.position - _lastTargetPosition).x;

			bool updateLookAheadTarget = Mathf.Abs (xMoveDelta) > lookAheadMoveThreshold;

			if (updateLookAheadTarget)
			{
				_lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign (xMoveDelta);
			}
			else
			{
				_lookAheadPos = Vector3.MoveTowards (_lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
			}

			Vector3 aheadTargetPos = _target.position + _lookAheadPos + Vector3.forward * _offsetZ;
			Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref _currentVelocity, damping);

			if(Time.deltaTime != 0)
			{
				transform.position = newPos + offset;
			}

			Vector3 temp = transform.position;
			temp.x = Mathf.Clamp(temp.x, -XBound, XBound);
			temp.y = Mathf.Clamp(temp.y, -YBound, YBound);
			temp.z = -10f;
			transform.position = temp;
			_lastTargetPosition = _target.position;
		}
    }

	public void LockCamera(bool value)
	{
		_locked = value;

		if(_locked)
		{
			this.gameObject.transform.position = new Vector3 (0, 0, -10);
			CameraAC.SetBool ("Growing", true);
		}
		else
		{
			CameraAC.SetBool ("Growing", false);
			Camera.main.orthographicSize = 6;
		}
	}
}

