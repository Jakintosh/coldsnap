using UnityEngine;

public class Player : MonoBehaviour {


	// ************** Information **************

	[System.Serializable] private class PlayerInput {
		public Direction movementDirection = Direction.NONE;
		public bool didJump = false;
	}
	[System.Serializable] private class PlayerAwareness {
		public bool? leftBecameObstructed = null;
		public bool? rightBecameObstructed = null;
		public bool? aboveBecameObstructed = null;
		public bool? belowBecameObstructed = null;
		public bool leftObstructed	= false;
		public bool rightObstructed	= false;
		public bool aboveObstructed	= false;
		public bool grounded		= false;
	}
	[System.Serializable] private class PlayerMovement {
		public bool IsRunning;
		public bool IsFrozen;
		public float JumpVelocity;
		public Direction MovementDirection;
		public float FrozenTimeRemaining;
	}

	// ************** Constants **************

	[SerializeField] private float JUMP_VELOCITY = 3f;
	[SerializeField] private float PLAYER_GRAVITY = -1f;
	[SerializeField] private float MOVEMENT_SPEED = 1f;

	private const float FROZEN_TIME_DURATION = 2.0f;

	// sizing
	private const float PLAYER_WIDTH = 1f;
	private const float PLAYER_HEIGHT = 1.5f;

	// proximity thresholds
	private const float BELOW_PROXIMITY_THRESHOLD = 0.025f;
	private const float ABOVE_PROXIMITY_THRESHOLD = 0.025f;
	private const float LEFT_PROXIMITY_THRESHOLD = 0.025f;
	private const float RIGHT_PROXIMITY_THRESHOLD = 0.025f;

	// ***************************************

	public int PlayerNumber { get; set; }
	public Direction Heading {
		get { return _movement.MovementDirection; }
	}

	// serialized fields
	[SerializeField] private LayerMask collisionLayerMask;
	[SerializeField] private bool debugMode;


	// player frame state
	private PlayerAwareness _lastAwareness;
	private PlayerAwareness _awareness;
	private PlayerInput _input;

	// consistent state
	private Frame _frame;
	private PlayerMovement _movement;

	public void ProjectileHit () {

		_movement.IsFrozen = true;
		_movement.FrozenTimeRemaining = FROZEN_TIME_DURATION;
	}
	private void Died () {
		Debug.Log( "Player " + PlayerNumber + " has died." );
		Destroy( gameObject );
	}

	// mono stuff
	private void Awake () {

		_frame = new Frame( PLAYER_WIDTH, PLAYER_HEIGHT );
		_movement = new PlayerMovement ();
		_awareness = new PlayerAwareness ();
	}
	private void Update () {

		if ( _movement.IsFrozen ) {
			_movement.FrozenTimeRemaining -= Time.deltaTime;
			if ( _movement.FrozenTimeRemaining <= 0f ) {
				_movement.IsFrozen = false;
			}
		}

		// update player frame
		_frame.Position = transform.position;

		PollInput ();
		CastRays ();
		UpdateMovement ();
		ProcessMovement ();
	}

	// get and process information
	private void PollInput () {

		_input = new PlayerInput ();

		var horizontalAxis = InputManager.GetHorizontalAxis( PlayerNumber );
		if ( horizontalAxis > 0 ) {
			_input.movementDirection = Direction.RIGHT;
		} else if ( horizontalAxis < 0 ) {
			_input.movementDirection = Direction.LEFT;
		}

		_input.didJump =  InputManager.GetJump( PlayerNumber );
	}
	private void CastRays () {

		_lastAwareness = _awareness;
		_awareness = new PlayerAwareness ();

		// check grounded
		RaycastHit2D hitDownL = Physics2D.Raycast( _frame.LL, Vector3.down, Mathf.Infinity, collisionLayerMask );
		RaycastHit2D hitDownC = Physics2D.Raycast( _frame.LC, Vector3.down, Mathf.Infinity, collisionLayerMask );
		RaycastHit2D hitDownR = Physics2D.Raycast( _frame.LR, Vector3.down, Mathf.Infinity, collisionLayerMask );
		if ( hitDownL.distance < BELOW_PROXIMITY_THRESHOLD ||
			 hitDownC.distance < BELOW_PROXIMITY_THRESHOLD ||
			 hitDownR.distance < BELOW_PROXIMITY_THRESHOLD ) {
			_awareness.grounded = true;
			if ( _lastAwareness.grounded == false ) {
				_awareness.belowBecameObstructed = true;
			}
		} else {
			_awareness.grounded = false;
			if ( _lastAwareness.grounded == true ) {
				_awareness.belowBecameObstructed = false;
			}
		}

		// check left
		RaycastHit2D hitLeftT = Physics2D.Raycast( _frame.UL, Vector3.left, Mathf.Infinity, collisionLayerMask );
		RaycastHit2D hitLeftM = Physics2D.Raycast( _frame.ML, Vector3.left, Mathf.Infinity, collisionLayerMask );
		RaycastHit2D hitLeftB = Physics2D.Raycast( _frame.LL, Vector3.left, Mathf.Infinity, collisionLayerMask );
		if ( hitLeftT.distance < LEFT_PROXIMITY_THRESHOLD ||
			 hitLeftM.distance < LEFT_PROXIMITY_THRESHOLD ||
			 hitLeftB.distance < LEFT_PROXIMITY_THRESHOLD ) {
			_awareness.leftObstructed = true;
			if ( _lastAwareness.leftObstructed == false ) {
				_awareness.leftBecameObstructed = true;
			}
		} else {
			_awareness.leftObstructed = false;
			if ( _lastAwareness.leftObstructed == true ) {
				_awareness.leftBecameObstructed = false;
			}
		}

		// check right
		RaycastHit2D hitRightT = Physics2D.Raycast( _frame.UR, Vector3.right, Mathf.Infinity, collisionLayerMask );
		RaycastHit2D hitRightM = Physics2D.Raycast( _frame.MR, Vector3.right, Mathf.Infinity, collisionLayerMask );
		RaycastHit2D hitRightB = Physics2D.Raycast( _frame.LR, Vector3.right, Mathf.Infinity, collisionLayerMask );
		if ( hitRightT.distance < RIGHT_PROXIMITY_THRESHOLD ||
			 hitRightM.distance < RIGHT_PROXIMITY_THRESHOLD ||
			 hitRightB.distance < RIGHT_PROXIMITY_THRESHOLD ) {
			_awareness.rightObstructed = true;
			if ( _lastAwareness.rightObstructed == false ) {
				_awareness.rightBecameObstructed = true;
			}
		} else {
			_awareness.rightObstructed = false;
			if ( _lastAwareness.rightObstructed == true ) {
				_awareness.rightBecameObstructed = false;
			}
		}

		// check up
		RaycastHit2D hitUpL = Physics2D.Raycast( _frame.UL, Vector3.up, Mathf.Infinity, collisionLayerMask );
		RaycastHit2D hitUpC = Physics2D.Raycast( _frame.UC, Vector3.up, Mathf.Infinity, collisionLayerMask );
		RaycastHit2D hitUpR = Physics2D.Raycast( _frame.UR, Vector3.up, Mathf.Infinity, collisionLayerMask );
		if ( hitUpL.distance < ABOVE_PROXIMITY_THRESHOLD ||
			 hitUpC.distance < ABOVE_PROXIMITY_THRESHOLD ||
			 hitUpR.distance < ABOVE_PROXIMITY_THRESHOLD ) {
			_awareness.aboveObstructed = true;
			if ( _lastAwareness.aboveObstructed == false ) {
				_awareness.aboveBecameObstructed = true;
			}
		} else {
			_awareness.aboveObstructed = false;
			if ( _lastAwareness.aboveObstructed == true ) {
				_awareness.aboveBecameObstructed = false;
			}
		}

		// draw debug lines
		if ( debugMode ) {
			Debug.DrawLine( _frame.LL, hitDownL.point, Color.green );
			Debug.DrawLine( _frame.LC, hitDownC.point, Color.green );
			Debug.DrawLine( _frame.LR, hitDownR.point, Color.green );

			Debug.DrawLine( _frame.UL, hitLeftT.point, Color.green );
			Debug.DrawLine( _frame.ML, hitLeftM.point, Color.green );
			Debug.DrawLine( _frame.LL, hitLeftB.point, Color.green );

			Debug.DrawLine( _frame.UR, hitRightT.point, Color.green );
			Debug.DrawLine( _frame.MR, hitRightM.point, Color.green );
			Debug.DrawLine( _frame.LR, hitRightB.point, Color.green );

			Debug.DrawLine( _frame.UL, hitUpL.point, Color.green );
			Debug.DrawLine( _frame.UC, hitUpC.point, Color.green );
			Debug.DrawLine( _frame.UR, hitUpR.point, Color.green );
			
			Debug.DrawLine( _frame.UL, _frame.UR, Color.red );
			Debug.DrawLine( _frame.UR, _frame.LR, Color.red );
			Debug.DrawLine( _frame.LR, _frame.LL, Color.red );
			Debug.DrawLine( _frame.LL, _frame.UL, Color.red );
		}
	}
	private void UpdateMovement () {

		// update jump velocity
		_movement.JumpVelocity += PLAYER_GRAVITY * Time.deltaTime;

		if ( _awareness.belowBecameObstructed.HasValue ) {
			if ( _awareness.belowBecameObstructed.Value ) {
				if ( _movement.IsFrozen ) {
					Died ();
				}
			}
		}

		// check stop jumping
		if ( _awareness.aboveBecameObstructed.HasValue ) {
			if ( _awareness.aboveBecameObstructed.Value == true ) {
				_movement.JumpVelocity = 0f;
			}
		}
		if ( _awareness.grounded ) {
			_movement.JumpVelocity = 0f;
		}

		// check start jumping
		if ( _awareness.grounded && !_awareness.aboveObstructed ) {
			if (  _input.didJump && !_movement.IsFrozen ) {
				_movement.JumpVelocity = JUMP_VELOCITY;
			}
		}

		// check movement
		switch ( _input.movementDirection ) {
			case Direction.LEFT:
				_movement.IsRunning = true && !_movement.IsFrozen;
				_movement.MovementDirection = Direction.LEFT;
				break;

			case Direction.RIGHT:
				_movement.IsRunning = true && !_movement.IsFrozen;
				_movement.MovementDirection = Direction.RIGHT;
				break;

			case Direction.NONE:
				_movement.IsRunning = false;
				break;
		}
	}
	private void ProcessMovement () {

		float deltaX = 0;
		float deltaY = 0;

		// update lateral movement
		if ( _movement.IsRunning ) {
			switch ( _movement.MovementDirection ) {
				case Direction.LEFT:
					if ( !_awareness.leftObstructed ) {
						deltaX = -1 * MOVEMENT_SPEED * Time.deltaTime;
					}
					break;
				case Direction.RIGHT:
					if ( !_awareness.rightObstructed ) {
						deltaX = MOVEMENT_SPEED * Time.deltaTime;
					}
					break;
			}
		}

		// update jumping
		deltaY = _movement.JumpVelocity * Time.deltaTime;

		// update position
		transform.position = transform.position + new Vector3( deltaX, deltaY );

		// adjust for wrapping
		float newX = transform.position.x;
		float newY = transform.position.y;
		if ( transform.position.x > 25.5f ) {
			newX -= 25f;
		} else if ( transform.position.x < -0.5f ) {
			newX += 25f;
		}
		if ( transform.position.y > 15.5f ) {
			newY -= 15f;
		} else if ( transform.position.y < -0.5f ) {
			newY += 15f;
		}

		transform.position = new Vector3( newX, newY );
	}
}
