using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ColorSwapController : MonoBehaviour {

	[SerializeField] private int maxSwapChances = 3;
	[ReadOnly] public int currentSwapChances;

	private GameObject player;
	private GameObject background;
	private GameObject[] enemies;
	private SpriteRenderer _enemyRenderer;
	private SpriteRenderer _playerRenderer;
	private SpriteRenderer _backgroundRenderer;
	private Color _greenBG = new Color (0f, 255f, 0f, 180f);
	private Color _redBG = new Color (255f, 0f, 0f, 180f);
	private Color _green = new Color (0f, 255f, 0f, 255f);
	private Color _red = new Color (255f, 0f, 0f, 255f);
	private bool _swap = false;

	public Text switchText;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		_playerRenderer = player.GetComponent <SpriteRenderer>();
		_playerRenderer.color = _red;

		background = GameObject.FindGameObjectWithTag ("Background");
		_backgroundRenderer = background.GetComponent <SpriteRenderer>();
		_backgroundRenderer.color = _greenBG;

		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		for(int i = 0; i < enemies.Length; i++ )
		{
			_enemyRenderer = enemies [i].GetComponent <SpriteRenderer> ();
			_enemyRenderer.color = _green;
		}
		currentSwapChances = maxSwapChances;
		switchText.text = "Switches: " + currentSwapChances;
	}
	
	// Update is called once per frame
	void Update () 
	{
		_swap = CrossPlatformInputManager.GetButtonDown ("Fire1"); 
		if(_swap && currentSwapChances > 0)
		{
			ColorSwap ();
			currentSwapChances--;
			switchText.text = "Switches: " + currentSwapChances;
		}
	}
		
	void ColorSwap ()
	{
		background = GameObject.FindGameObjectWithTag ("Background");
		_backgroundRenderer = background.GetComponent <SpriteRenderer>();
		if(_backgroundRenderer.color.Equals (_greenBG))
		{
			_backgroundRenderer.color = _redBG;
			_playerRenderer.enabled = false;
			for(int i = 0; i < enemies.Length; i++ )
			{
				_enemyRenderer = enemies [i].GetComponent <SpriteRenderer> ();
				_enemyRenderer.enabled = true;
			}
		}
		else
		{
			_backgroundRenderer.color = _greenBG;
			_playerRenderer.enabled = true;
			for(int i = 0; i < enemies.Length; i++ )
			{
				_enemyRenderer = enemies [i].GetComponent <SpriteRenderer> ();
				_enemyRenderer.enabled = false;
			}
		}
	}
}
