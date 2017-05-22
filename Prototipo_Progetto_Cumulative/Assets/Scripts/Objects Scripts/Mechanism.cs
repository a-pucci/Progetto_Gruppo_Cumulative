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

	private int _gears = 0;
	private bool _keyUsed = false;
	private SpriteRenderer _color;
	private GameObject _newGear;

	void Start()
	{
		_color = this.gameObject.GetComponent<SpriteRenderer> ();
		_color.color = new Color (0, 0, 0);
	}

	public bool InsertKey()
	{
		if(_gears >= MaxGears)
		{
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
			_newGear.transform.parent = this.transform;

			break;

		case 1:
			_newGear = Instantiate (Gear);
			_newGear.GetComponent<Collider2D> ().enabled = false;
			_newGear.transform.position = new Vector3 (this.transform.position.x + XOffset, this.transform.position.y + YOffset, this.transform.position.z);
			_newGear.transform.parent = this.transform;

			break;

		case 2:
			_newGear = Instantiate (Gear);
			_newGear.GetComponent<Collider2D> ().enabled = false;
			_newGear.transform.position = new Vector3 (this.transform.position.x + XOffset, this.transform.position.y + YOffset + 0.5f, this.transform.position.z);
			_newGear.transform.parent = this.transform;

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
			SceneManager.LoadScene (NextLevel);
	}
}
