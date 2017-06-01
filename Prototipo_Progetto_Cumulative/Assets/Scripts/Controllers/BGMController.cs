using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMController : MonoBehaviour
{
	private	AudioSource source;
	private StageSwapController stageSwap;
	public AudioMixerGroup bgmMixerA;
	public AudioMixerGroup bgmMixerB;

	void Start()
	{
		stageSwap = GameObject.FindGameObjectWithTag ("StageSwap").GetComponent<StageSwapController> ();
		source = this.GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (stageSwap.toggle) {
			source.outputAudioMixerGroup = bgmMixerB;
		} else {
			source.outputAudioMixerGroup = bgmMixerA;
		}
	}
}
