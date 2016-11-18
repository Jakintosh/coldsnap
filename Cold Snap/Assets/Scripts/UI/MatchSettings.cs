using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MatchSettings : MonoBehaviour {

	[Header("Images")]
	[SerializeField] private Image _player1Portrait;
	[SerializeField] private Image _player2Portrait;
	[SerializeField] private Image _player1Ready;
	[SerializeField] private Image _player2Ready;

	[Header("Canvas Group")]
	[SerializeField] private CanvasGroup _canvasGroup;

	private bool _p1IsReady = false;
	private bool _p2IsReady = false;

	private static float TIMER = 1.0f;
	private bool isFading = false;

	private void Update () {

		if ( !isFading ) {
			CheckPlayerReady ();
		}
		CheckStartupTimer ();
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
			StartCoroutine( Confirmed() );
		}
	}

	private IEnumerator Confirmed () {

		isFading = true;

		yield return new WaitForSeconds( TIMER );


		_p1IsReady = false;
		_p2IsReady = false;
		_player1Ready.color = new Color(0,0,0,0);
		_player2Ready.color = new Color(0,0,0,0);
		NotificationCenter.PostNotification( Notification.MATCH_SETTINGS_CONFIRMED );
	}

	private void CheckStartupTimer () {

		if ( isFading ) {
			_canvasGroup.alpha -= ( Time.deltaTime / TIMER );
		}
	}
}



