using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] ParticleSystem CollisionParticleEffect;

	private GameObject _owner;
	private float _speed;
	private Vector3 _direction;

	public void Launch ( float speed, Direction direction, GameObject owner ) {

		_speed = speed;
		_owner = owner;
		switch ( direction ) {
			case Direction.LEFT:
				_direction = Vector3.left;
				gameObject.GetComponent<SpriteRenderer>().flipX = true;
				break;
			case Direction.RIGHT:
				_direction = Vector3.right;
				gameObject.GetComponent<SpriteRenderer>().flipX = false;
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

		Instantiate(CollisionParticleEffect).transform.position = gameObject.transform.position;
		Destroy( gameObject );
	}
}
