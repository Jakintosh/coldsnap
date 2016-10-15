using UnityEngine;

[RequireComponent(typeof(Player))]
public class Shooting : MonoBehaviour {

	[SerializeField] private float _shootSpeed = 10f;
	private const float SHOOT_COOLDOWN = 0.66f;

	private float _timeUntilShoot;

	private Player _player;
	private AnimationManager _playerAnimationManager;

	private void Start () {

		_playerAnimationManager = GetComponent<AnimationManager>();
		_player = GetComponent<Player>();
	}
	private void Update () {

		if ( _timeUntilShoot > 0f ) {
			_timeUntilShoot -= Time.deltaTime;
		}

		if ( _timeUntilShoot <= 0f ) {
			if ( InputManager.GetShoot( _player.PlayerNumber ) ) {
				Shoot ();
			}
		}
	}
	private void Shoot () {

		_timeUntilShoot = SHOOT_COOLDOWN;
		var projectileGO = Object.Instantiate( Game.Resources.Projectile.Default, transform.position, Quaternion.identity ) as GameObject;
		var projectile = projectileGO.GetComponent<Projectile>();
		projectile.Launch( _shootSpeed, _player.Heading, _player.gameObject );
		_playerAnimationManager.RangedAttack();
	}
}
