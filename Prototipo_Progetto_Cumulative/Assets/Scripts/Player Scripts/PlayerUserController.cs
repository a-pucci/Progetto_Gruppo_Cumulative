using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (PlayerMovement))]
public class PlayerUserController : MonoBehaviour
{
	private PlayerMovement _character;
	private bool _jump;


	private void Awake()
	{
		_character = GetComponent<PlayerMovement>();
	}


	private void Update()
	{
		if (!_jump) {
			// Read the jump input in Update so button presses aren't missed.
			_jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}
	}


	private void FixedUpdate() {
		// Read the inputs.
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		// Pass all parameters to the character control script.
		_character.Move(h, _jump);
		_jump = false;
	}
}
