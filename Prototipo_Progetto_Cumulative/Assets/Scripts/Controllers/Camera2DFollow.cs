using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
	[Header("Target")]
    public Transform target;
    public float damping = 0;
    public float lookAheadFactor = 0;
    public float lookAheadReturnSpeed = 0f;
    public float lookAheadMoveThreshold = 0f;
	public Vector3 offset;

	[Header("Camera Bounds")]
	public float XBound;
	public float YBound;	

    private float _offsetZ;
    private Vector3 _lastTargetPosition;
    private Vector3 _currentVelocity;
    private Vector3 _lookAheadPos;

	private HealthManager _playerHealth;

    // Use this for initialization
    private void Start()
    {
		_lastTargetPosition = target.position;
		_offsetZ = (transform.position - target.position).z;
        transform.parent = null;
		_playerHealth = target.gameObject.GetComponent<HealthManager> ();
    }


    // Update is called once per frame
    private void Update()
{
		if (!_playerHealth.PlayerDead)
		{
			// only update lookahead pos if accelerating or changed direction
			float xMoveDelta = (target.position - _lastTargetPosition).x;

			bool updateLookAheadTarget = Mathf.Abs (xMoveDelta) > lookAheadMoveThreshold;

			if (updateLookAheadTarget)
			{
				_lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign (xMoveDelta);
			}
			else
			{
				_lookAheadPos = Vector3.MoveTowards (_lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
			}

			Vector3 aheadTargetPos = target.position + _lookAheadPos + Vector3.forward * _offsetZ;
			Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref _currentVelocity, damping);

			transform.position = newPos + offset;

			Vector3 temp = transform.position;
			temp.x = Mathf.Clamp(temp.x, -XBound, XBound);
			temp.y = Mathf.Clamp(temp.y, -YBound, YBound);
			temp.z = -10f;
			transform.position = temp;
			_lastTargetPosition = target.position;
		}
    }
}

