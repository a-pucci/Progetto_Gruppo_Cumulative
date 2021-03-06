﻿using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	[SerializeField] private float _maxSpeed = 7f;                    // The fastest the player can travel in the x axis.
	[SerializeField] private float _jumpForce = 400f;                  // Amount of force added when the player jumps.
	[SerializeField] private bool _airControl = false;                 // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask _whatIsGround;                  // A mask determining what is ground to the character

	[Header("Character Audio")]
	public AudioClip JumpClip;
	[Range(0.0f, 1.0f)] public float JumpVolume = 0.8f;
	public AudioClip LandingClip;
	[Range(0.0f, 1.0f)] public float LandingVolume = 0.8f;

	private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .3f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private Animator _playerAnim;
	private SFXController _sfxManager;
	private bool _lastGrounded;

	private bool _facingRight = true;

	private void Awake()
	{
		// Setting up references.
		m_GroundCheck = transform.Find("GroundCheck");
		m_Rigidbody2D = GetComponent<Rigidbody2D> ();
		_playerAnim = gameObject.transform.FindChild("Sprite").GetComponent <Animator> ();
		_sfxManager = GameObject.FindGameObjectWithTag ("SFXManager").GetComponent<SFXController> ();
	}


	private void FixedUpdate()
	{
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, _whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders [i].gameObject != gameObject)
			{

				m_Grounded = true;
			}

		}
		if(m_Grounded)
		{
			if (_lastGrounded == false)
			{
				_sfxManager.PlaySFX (LandingClip, LandingVolume);
			}

			_playerAnim.SetBool ("Ground", true);
		}
		else
		{
			_playerAnim.SetBool ("Ground", false);
		}

		_lastGrounded = m_Grounded;
	}


	public void Move(float move, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || _airControl)
		{

			// Move the character
			m_Rigidbody2D.velocity = new Vector2(move*_maxSpeed, m_Rigidbody2D.velocity.y);
		
			_playerAnim.SetFloat("Speed", Mathf.Abs(move));

			if (move > 0 && !_facingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && _facingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			_playerAnim.SetTrigger ("Jump");
			_sfxManager.PlaySFX (JumpClip, JumpVolume);
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, _jumpForce));
		}

	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_facingRight = !_facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool isGrounded()
	{
		return m_Grounded;
	}

	public void IncreaseMovementSpeed(float value)
	{
		_maxSpeed = _maxSpeed + value;
	}
}
