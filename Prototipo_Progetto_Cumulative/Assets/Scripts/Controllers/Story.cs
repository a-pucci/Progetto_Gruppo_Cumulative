using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Story : MonoBehaviour 
{
	
	private Text _storyText;
	[TextArea]	public string[] _stories;
	public Image[] Avatars;
	public float WaitTime;
	private bool _canGoNext = false;
	private int _count = 0;

	// Use this for initialization
	void Start () 
	{	
		for(int i = 1; i < Avatars.Length; i++)
		{
			Avatars [i].enabled = false;
		}

		_storyText = GetComponent<Text> ();
		_storyText.text = _stories [_count];
		Avatars [_count].enabled = true;
			
		StartCoroutine (NextStory());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(CrossPlatformInputManager.GetButtonDown("Drop") && _canGoNext)
		{
			if(_count < _stories.Length-1)
			{
				_count++;
				_storyText.text = _stories [_count];

				Avatars [_count-1].enabled = false;
				Avatars [_count].enabled = true;

				_canGoNext = false;
				StartCoroutine (NextStory ());
			}
			else
			{
				LevelManager.GoNextLevel ();
			}
		}
	}

	private IEnumerator NextStory()
	{	
		yield return new WaitForSeconds (WaitTime);
		_canGoNext = true;
	}

}
