using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class HealthManager : MonoBehaviour
{

	[SerializeField] private int _health = 1;
	[SerializeField] private int _currentHealth = 0;

	public bool playerDead = false;

	public Text healthText;
	public Text gameOverText;

	private SpriteRenderer _spriteRenderer;

	void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_currentHealth = _health;
		healthText.text = "Health: " + _currentHealth;
		gameOverText.enabled = false;
		playerDead = false;
	}

	void Update()
	{
		if (playerDead && CrossPlatformInputManager.GetButtonDown("Restart")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
			Time.timeScale = 1;
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag ("Enemy")) {
			TakeDamage ();
		}
	}

	public void TakeDamage()
	{
		_currentHealth -= 1;
		if (_currentHealth <= 0) {
			Death ();
		}
		healthText.text = "Health: " + _currentHealth;
	}

	void Death ()
	{
		Time.timeScale = 0;
		_spriteRenderer.enabled = false;
		gameOverText.enabled = true;
		playerDead = true;
	}
}