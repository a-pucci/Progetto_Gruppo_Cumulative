using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXController : MonoBehaviour
{
	private AudioSource source;

	void Start() 
	{
		source = this.GetComponent<AudioSource> ();
	}

	public void PlaySFX(AudioClip sfx) 
	{
		source.clip = sfx;
		source.Play ();
		source.clip = null;
	}
}
