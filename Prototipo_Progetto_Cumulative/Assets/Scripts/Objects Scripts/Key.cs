using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : StageObject
{
	void Start()
	{
		base.ID = (int)IDList.ID.Key;
	}
}
