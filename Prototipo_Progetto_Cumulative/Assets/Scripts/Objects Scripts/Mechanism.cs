using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mechanism : StageObject
{
	[Header("-- MECHANISM --")]
	public string NextLevel;

	[Header("Gear Settings")]
	public GameObject Gear;
	public int MaxGears;
	public float XOffset;
	public float YOffset;

	[Header("Key Settings")]
	public GameObject Key;
	public Vector3 KeyOffset;

	[Header("Closure Settings")]
	public float WaitingTime;

	private int _gears = 0;
	private bool _keyUsed = false;
	private SpriteRenderer _color;
	private GameObject _newGear;
	private GameObject _newKey;
	private CameraEnd _camera;

	void Start()
	{
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraEnd> ();
		_color = this.gameObject.GetComponent<SpriteRenderer> ();
		_color.color = new Color (0, 0, 0);
	}

	public bool InsertKey()
	{
		if(_gears >= MaxGears)
		{
			_newKey = Instantiate (Key);
			_newKey.GetComponent<Collider2D> ().enabled = false;
			_newKey.transform.position = this.transform.position + KeyOffset;
			_newKey.transform.rotation = new Quaternion (0, 0, 0, 0);
			_newKey.transform.parent = this.transform.parent;

			_keyUsed = true;
			_color.color = new Color (255, 255, 255);
			CloseScene ();
		}

		return _keyUsed;
	}

	public int InsertGear()
	{
		switch(_gears)
		{
		case 0:
			_newGear = Instantiate (Gear);
			_newGear.GetComponent<Collider2D> ().enabled = false;
			_newGear.transform.position = new Vector3 (this.transform.position.x + XOffset, this.transform.position.y + YOffset - 0.5f, this.transform.position.z);
			_newGear.transform.parent = this.transform.parent;
			_newGear.GetComponent <Gear> ().RotateRight ();

			break;

		case 1:
			_newGear = Instantiate (Gear);
			_newGear.GetComponent<Collider2D> ().enabled = false;
			_newGear.transform.position = new Vector3 (this.transform.position.x + XOffset, this.transform.position.y + YOffset, this.transform.position.z);
			_newGear.transform.parent = this.transform.parent;
			_newGear.GetComponent <Gear> ().RotateLeft ();

			break;

		case 2:
			_newGear = Instantiate (Gear);
			_newGear.GetComponent<Collider2D> ().enabled = false;
			_newGear.transform.position = new Vector3 (this.transform.position.x + XOffset, this.transform.position.y + YOffset + 0.5f, this.transform.position.z);
			_newGear.transform.parent = this.transform.parent;
			_newGear.GetComponent <Gear> ().RotateRight ();

			break;
		}

		if(_gears <= MaxGears)
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

	private IEnumerator ChangeScene()
	{
		while (_camera.IsShowing() == true)
		{
			yield return new WaitForSeconds (WaitingTime);
		}
		SceneManager.LoadScene (NextLevel);
	}
}
