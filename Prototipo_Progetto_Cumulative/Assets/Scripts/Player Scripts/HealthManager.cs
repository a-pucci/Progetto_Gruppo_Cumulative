using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class HealthManager : MonoBehaviour
{
	[Header("Death Audio")]
	public AudioClip DeathClip;

	public bool PlayerDead = false;
	public Text GameOverText;

	private SpriteRenderer _spriteRenderer;
	private int _health = 1;

	private SFXController _sfxManager;

	void Start()
	{
		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		GameOverText.enabled = false;
		PlayerDead = false;
	}

	void Update()
	{
		if (PlayerDead && CrossPlatformInputManager.GetButtonDown("Restart")) 
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
			Time.timeScale = 1;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag ("Enemy") && collision.gameObject.GetComponent<EnemyMovement>().CanMove()) 
		{
			TakeDamage ();
		}
	}

	public void TakeDamage()
	{
		_health -= 1;
		if (_health <= 0) 
		{
			Death ();
		}
	}

	void Death ()
	{
		_sfxManager.PlaySFX (DeathClip);
		Time.timeScale = 0;
		_spriteRenderer.enabled = false;
		GameOverText.enabled = true;
		PlayerDead = true;
	}
}