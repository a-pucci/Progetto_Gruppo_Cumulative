using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mechanism : StageObject
{
	[Header("-- MECHANISM --")]
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

	[Header("Audio")]
	public AudioClip InsertMechanismClip;
	[Range(0.0f, 1.0f)] public float InsertMechanismVolume = 0.8f;

	private int _gears = 0;
	private int _maxGears = 3;
	private int _counter = 0;
	private bool _keyUsed = false;

	private GameObject _newGear;
	private GameObject _newKey;
	private CameraEnd _camera;
	private PlayerPickup _player;

	private List<GameObject> _gearsPrefab;
	private List<Vector3> _gearsPos;

	private SFXController _sfxManager;

	void Start()
	{
		base.ID = (int)IDList.ID.Mechanism;
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraEnd> ();
		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
		_player = GameObject.FindGameObjectWithTag ("Player").GetComponent <PlayerPickup> ();

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
			_camera.StartClose ();
		}

		return _keyUsed;
	}

	public void InsertGear()
	{
		if(_gears < _maxGears)
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
			_gears ++;
		}
	}

	public override void Interact (ref GameObject other)
	{
		if(CanInteract (other))
		{
			if(other.GetComponent<StageObject>().ID == (int)IDList.ID.Key)
			{
				InsertKey ();
				_player.RemoveItemFromInventory ();
			}
			else if(other.GetComponent<StageObject>().ID == (int)IDList.ID.Gear)
			{
				InsertGear ();
				if(_gears <= _maxGears)
				{
					_sfxManager.PlaySFX (InsertMechanismClip, InsertMechanismVolume);
					_player.RemoveItemFromInventory ();
				}
			}
		}
	}

	public override bool CanInteract (GameObject other)
	{
		bool canInteract = false;
		if(other.GetComponent<StageObject> () != null)
		{
			int otherID = other.GetComponent<StageObject> ().ID;
			if((_gears < _maxGears && otherID == (int)IDList.ID.Gear) || (otherID == (int)IDList.ID.Key && _gears >= _maxGears))
			{
				canInteract = true;
			}		
		}
		return canInteract;
	}
}
