using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class HealthManager : MonoBehaviour
{
	public bool playerDead = false;
	public Text gameOverText;

	private SpriteRenderer _spriteRenderer;
	private int _health = 1;

	void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		gameOverText.enabled = false;
		playerDead = false;
	}

	void Update()
	{
		if (playerDead && CrossPlatformInputManager.GetButtonDown("Restart")) 
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
		Time.timeScale = 0;
		_spriteRenderer.enabled = false;
		gameOverText.enabled = true;
		playerDead = true;
	}
}