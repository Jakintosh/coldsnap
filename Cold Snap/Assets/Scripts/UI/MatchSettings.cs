using UnityEngine;
using UnityEngine.UI;

public class MatchSettings : MonoBehaviour {

	[Header("Images")]
	[SerializeField] private Image _player1Portrait;
	[SerializeField] private Image _player2Portrait;
	[SerializeField] private Image _player1Ready;
	[SerializeField] private Image _player2Ready;

	[Header("Toggles")]
	[SerializeField] private Toggle _1LifeToggle;
	[SerializeField] private Toggle _3LifeToggle;
	[SerializeField] private Toggle _5LifeToggle;

	private Selectable _selectedToggle;

	private float _selectionCooldown = 0f;

	private bool _p1IsReady = false;
	private bool _p2IsReady = false;

	private void Start () {

		_3LifeToggle.isOn = true;
		_selectedToggle = _3LifeToggle.GetComponent<Selectable>();
	}

	private void Update () {

		// CheckToggleSelection ();
		CheckPlayerReady ();
	}

	private void CheckToggleSelection () {

		// get new toggle and update cooldown
		Selectable newToggle = null;
		var horizontal = Input.GetAxis( "Horizontal" );
		if ( horizontal > 0 ) {
			newToggle = _selectedToggle.FindSelectableOnRight ();
		} else if ( horizontal < 0 ) {
			newToggle = _selectedToggle.FindSelectableOnLeft ();
		} else {
			_selectionCooldown = 0f;
		}

		// decrement cooldown
		if ( _selectionCooldown > 0f ) {
			_selectionCooldown -= Time.deltaTime;
			return;
		}

		// assign new toggle
		if ( newToggle != null ) {
			newToggle.GetComponent<Toggle>().isOn = true;
			_selectedToggle = newToggle;
			_selectionCooldown = 0.25f;
		}
	}

	private void CheckPlayerReady () {

		if ( InputManager.GetShoot( 1 ) ) {
			_p1IsReady = !_p1IsReady;
			_player1Ready.color = _p1IsReady ? new Color(1,1,1,1) : new Color(0,0,0,0);
		}

		if ( InputManager.GetShoot( 2 ) ) {
			_p2IsReady = !_p2IsReady;
			_player2Ready.color = _p2IsReady ? new Color(1,1,1,1) : new Color(0,0,0,0);
		}

		if ( _p1IsReady && _p2IsReady ) {
			_p1IsReady = false;
			_p2IsReady = false;
			_player1Ready.color = new Color(0,0,0,0);
			_player2Ready.color = new Color(0,0,0,0);
			NotificationCenter.PostNotification( Notification.MATCH_SETTINGS_CONFIRMED );
		}
	}
}



