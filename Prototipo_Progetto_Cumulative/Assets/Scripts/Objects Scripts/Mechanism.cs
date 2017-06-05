using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mechanism : StageObject
{
	[Header("-- MECHANISM --")]
	public string NextLevel;

	[Header("Gear Settings")]
	public GameObject GearSmall;
	public GameObject GearMedium;
	public GameObject GearLarge;
	public GameObject GearFixed;

	public Vector3 SmallPosition;
	public Vector3 MediumPosition;
	public Vector3 LargePosition;
	public Vector3 FixedPosition;

	public int GearsNeeded;

	[Header("Key Settings")]
	public GameObject Key;
	public Vector3 KeyPosition;

	[Header("Closure Settings")]
	public float WaitingTime;

	private int _gears = 0;
	private int _maxGears = 3;
	private int _counter = 0;
	private bool _keyUsed = false;

	private GameObject _newGear;
	private GameObject _newKey;
	private CameraEnd _camera;

	private List<GameObject> _gearsPrefab;
	private List<Vector3> _gearsPos;

	void Start()
	{
		base.ID = (int)IDList.ID.Mechanism;
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraEnd> ();

		_gears = _maxGears - GearsNeeded;

		_gearsPrefab = new List<GameObject> ();
		_gearsPrefab.Add (GearLarge);
		_gearsPrefab.Add (GearMedium);
		_gearsPrefab.Add (GearSmall);

		_gearsPos = new List<Vector3> ();
		_gearsPos.Add (LargePosition);
		_gearsPos.Add (MediumPosition);
		_gearsPos.Add (SmallPosition);

		_newGear = Instantiate (GearFixed);
		_newGear.GetComponent<Collider2D> ().enabled = false;
		_newGear.transform.position = this.transform.position + FixedPosition;
		_newGear.transform.parent = this.transform.parent;
		_newGear.GetComponent <Gear> ().RotateRight ();

		for(int i = GearsNeeded; i < 3; i++)
		{
			_newGear = Instantiate (_gearsPrefab[i]);
			_newGear.GetComponent<Collider2D> ().enabled = false;
			_newGear.transform.position = this.transform.position + _gearsPos[i];
			_newGear.transform.parent = this.transform.parent;
			Vector3 scale = new Vector3 (0.3f, 0.3f, 1);
			_newGear.transform.localScale = scale;

			if(i == 0)
			{
				_newGear.GetComponent <Gear> ().RotateRight ();
			}
			else
			{
				_newGear.GetComponent <Gear> ().RotateLeft ();
			}
		}
	}

	public bool InsertKey()
	{
		if(_gears >= _maxGears)
		{
			_newKey = Instantiate (Key);
			_newKey.GetComponent<Collider2D> ().enabled = false;
			_newKey.transform.position = this.transform.position + KeyPosition;
			_newKey.transform.rotation = new Quaternion (0, 0, 0, 0);
			_newKey.transform.parent = this.transform.parent;
			Vector3 scale = new Vector3 (-0.3f, 0.3f, 1);
			_newKey.transform.localScale = scale;

			_keyUsed = true;
			CloseScene ();
		}

		return _keyUsed;
	}

	public int InsertGear()
	{
		if(_gears <= GearsNeeded)
		{
			_newGear = Instantiate (_gearsPrefab[_counter]);
			_newGear.GetComponent<Collider2D> ().enabled = false;
			_newGear.transform.position = this.transform.position + _gearsPos[_counter];
			_newGear.transform.parent = this.transform.parent;
			Vector3 scale = new Vector3 (0.3f, 0.3f, 1);
			_newGear.transform.localScale = scale;

			if(_counter == 0)
			{
				_newGear.GetComponent <Gear> ().RotateRight ();
			}
			else
			{
				_newGear.GetComponent <Gear> ().RotateLeft ();
			}

			_counter++;
		}

		if(_gears <= _maxGears)
		{
			_gears += 1;
		}

		return _gears;
	}

	private void CloseScene()
	{
		_camera.StartClose ();
		StartCoroutine (ChangeScene ());
	}

	public int GetMaxGears()
	{
		return _maxGears;
	}

	private IEnumerator ChangeScene()
	{
		while (_camera.IsShowing() == true)
		{
			yield return new WaitForSeconds (WaitingTime);
		}
		SceneManager.LoadScene (NextLevel);
	}
}
