using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

	[SerializeField] private int _startingHealth = 3;
	[SerializeField] private int _currentHealth = 0;

	public Text healthText;

	private SpriteRenderer _spriteRenderer;

	void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_currentHealth = _startingHealth;
		healthText.text = "Health: " + _currentHealth;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Enemy")) {
			_currentHealth -= 1;
			if (_currentHealth <= 0) {
				Time.timeScale = 0;
				_spriteRenderer.enabled = false;
			}
			healthText.text = "Health: " + _currentHealth;
		}
	}
}
