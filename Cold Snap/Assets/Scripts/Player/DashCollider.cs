using UnityEngine;

public class DashCollider : MonoBehaviour {

	private int _wallCollisions;
	private bool _collidingWithPlayer;
	private Player _collidedPlayer;

	public bool WillHitPlayer {
		get { return _collidingWithPlayer; }
	}
	public bool WillHitWall {
		get { return _wallCollisions != 0; }
	}
	public Player TargetedPlayer {
		get { return _collidedPlayer; }
	}

	private void OnTriggerEnter2D ( Collider2D other ) {

		if ( other.CompareTag( "Player" ) ) {
			_collidingWithPlayer = true;
			_collidedPlayer = other.GetComponent<Player>();
			Debug.Log("Player enter");
		}
	}

	private void Update () {

		_wallCollisions = _triggersThisFrame;
		_triggersThisFrame = 0;
	}

	private int _triggersThisFrame = 0;
	private void OnTriggerStay2D ( Collider2D other ) {
		if ( other.CompareTag( "PlayerCollidable" ) ) {
			_triggersThisFrame++;
		}
	}

	private void OnTriggerExit2D ( Collider2D other ) {

		if ( other.CompareTag( "Player" ) ) {
			_collidingWithPlayer = false;
			_collidedPlayer = null;
		}
	}
}
