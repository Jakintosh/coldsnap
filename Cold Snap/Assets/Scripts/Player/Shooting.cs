using UnityEngine;

[RequireComponent(typeof(Player))]
public class Shooting : MonoBehaviour {

	[SerializeField] private float _shootSpeed = 10f;

	private Player _player;

	private void Start () {
		
		_player = GetComponent<Player>();
	}
	private void Update () {

		if ( InputManager.GetShoot( _player.PlayerNumber ) ) {
			Shoot ();
		}
	}
	private void Shoot () {

		var projectileGO = Object.Instantiate( Game.Resources.Projectile.Default, transform.position, Quaternion.identity ) as GameObject;
		var projectile = projectileGO.GetComponent<Projectile>();
		projectile.Launch( _shootSpeed, _player.Heading, _player.gameObject );
	}
}
