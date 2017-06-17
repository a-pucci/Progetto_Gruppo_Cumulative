using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : MonoBehaviour {

	private SFXController _sfxManager;
	public List<AudioClip> stepsList;
	[Range(0.0f, 1.0f)] public float StepsVolume = 0.4f;

	private void Awake()
	{
		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
	}

	public void PlayStep()
	{
		int RandomValue = UnityEngine.Random.Range (0, stepsList.Count);
		_sfxManager.PlaySFX (stepsList [RandomValue], StepsVolume);
	}

}
