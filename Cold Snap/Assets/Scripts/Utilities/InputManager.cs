using UnityEngine;

public static class InputManager {

	public static float GetHorizontalAxis ( int joystick ) {
		switch ( joystick ) {
			case 1:
				return Input.GetAxis( "Horizontal1" );
			case 2:
				return Input.GetAxis( "Horizontal2" );
		}
		return 0f;
	}

	public static bool GetJump ( int joystick ) {
		switch ( joystick ) {
			case 1:
				return Input.GetButtonDown( "Jump1" );
			case 2:
				return Input.GetButtonDown( "Jump2" );
		}
		return false;
	}

	public static bool GetShoot ( int joystick ) {
		switch ( joystick ) {
			case 1:
				return Input.GetButtonDown( "Fire1" );
			case 2:
				return Input.GetButtonDown( "Fire2" );
		}
		return false;
	}

	public static bool GetLeftDash ( int joystick ) {
		switch ( joystick ) {
			case 1:
				return Input.GetButtonDown( "LeftDash1" );
			case 2:
				return Input.GetButtonDown( "LeftDash2" );
		}
		return false;
	}

	public static bool GetRightDash ( int joystick ) {
		switch ( joystick ) {
			case 1:
				return Input.GetButtonDown( "RightDash1" );
			case 2:
				return Input.GetButtonDown( "RightDash2" );
		}
		return false;
	}

	public static bool GetQuit () {

		return Input.GetButtonDown( "Quit" );
	}

	public static bool GetRematch () {

		return Input.GetButtonDown( "Rematch" );
	}
}
