using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

	[SerializeField] private int m_StartingHealth = 3;
	[SerializeField] private int m_CurrentHealth = 0;

	private SpriteRenderer m_SpriteRenderer;

	void Start()
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		m_CurrentHealth = m_StartingHealth;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag ("Enemy")) {
			m_CurrentHealth -= 1;
			if (m_CurrentHealth <= 0) {
				Time.timeScale = 0;
				m_SpriteRenderer.enabled = false;
			}
		}
	}
}
