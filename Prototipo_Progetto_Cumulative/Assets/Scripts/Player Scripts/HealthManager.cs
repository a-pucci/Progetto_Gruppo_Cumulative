using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class HealthManager : MonoBehaviour
{

	[SerializeField] private int _startingHealth = 3;
	[SerializeField] private int _currentHealth = 0;
	[SerializeField] private int _numberOfFlashes = 5;

	[ReadOnly] public bool invulnerable = false;
	[ReadOnly] public bool playerDead = false;

	public Text healthText;
	public Text gameOverText;

	private SpriteRenderer _spriteRenderer;

	void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_currentHealth = _startingHealth;
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
		if (collision.gameObject.CompareTag ("Enemy") && !invulnerable) {
			TakeDamage ();
		}
	}

	void TakeDamage()
	{
		_currentHealth -= 1;
		StartCoroutine(GoInvulnerable ());
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
		
	IEnumerator GoInvulnerable () 
	{
		if (!invulnerable) {
			invulnerable = true;
			Color transparent = new Color(255,255,255,0);
			Color current = _spriteRenderer.color;
			for (int i = 0; i < _numberOfFlashes; i++) {
				_spriteRenderer.color = transparent;
				yield return new WaitForSeconds(.1f);
				_spriteRenderer.color = current;
				yield return new WaitForSeconds(.1f);
			}
			invulnerable = false;
		}
	}
}