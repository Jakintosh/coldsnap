using UnityEngine;

public class Projectile : MonoBehaviour {

	private GameObject _owner;
	private float _speed;
	private Vector3 _direction;

	public void Launch ( float speed, Direction direction, GameObject owner ) {

		_speed = speed;
		_owner = owner;
		switch ( direction ) {
			case Direction.LEFT:
				_direction = Vector3.left;
				break;
			case Direction.RIGHT:
				_direction = Vector3.right;
				break;
		}
	}
	private void Update () {

		transform.Translate( _direction * _speed * Time.deltaTime );
	}

	private void OnTriggerEnter2D ( Collider2D other ) {

		if ( other.gameObject == _owner ) {
			return;
		}

		if ( other.gameObject.CompareTag( "Player" ) ) {
			var player = other.gameObject.GetComponent<Player>();
			player.ProjectileHit ();
		}
		Destroy( gameObject );
	}
}
