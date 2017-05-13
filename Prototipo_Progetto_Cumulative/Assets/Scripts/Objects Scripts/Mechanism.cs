using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mechanism : StageObject
{
	public string NextLevel;
	public int MaxGears;
	private int _gears = 0;
	private bool _hasKey = false;

	private SpriteRenderer _color;

	void Start()
	{
		_color = this.gameObject.GetComponent<SpriteRenderer> ();
		_color.color = new Color (0, 0, 0);
	}

	public bool InsertKey()
	{
		if(_gears == MaxGears)
		{
			_hasKey = true;

			_color.color = new Color (255, 255, 255);
		}
		return _hasKey;
	}

	public int InsertGear()
	{
		if(_gears <= MaxGears)
		{
			_gears += 1;
		}

		switch(_gears)
		{
		case 1:
			_color.color = new Color (50, 50, 50);
			break;

		case 2:
			_color.color = new Color (100, 100, 100);
			break;

		case 3:
			_color.color = new Color (150, 150, 150);
			break;
		}

		return _gears;
	}

	private void CloseScene()
	{
		if(_gears == 3 && _hasKey)
		{
			SceneManager.LoadScene (NextLevel);
		}
	}
}
