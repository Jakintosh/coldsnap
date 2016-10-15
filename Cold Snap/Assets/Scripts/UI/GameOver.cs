using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	[SerializeField] private Text _gameOverText;

	private void Update () {

		if ( Game.Instance.player1Alive ) {
			_gameOverText.text = "Player 2 died :(";
		} else if ( Game.Instance.player2Alive ) {
			_gameOverText.text = "Player 1 died :(";
		} else {
			_gameOverText.text = "Everyone died :(";
		}

		if ( InputManager.GetJump( 1 ) || InputManager.GetJump( 2 ) ) {
			NotificationCenter.PostNotification( Notification.REMATCH );
		}

		if ( InputManager.GetShoot( 1 ) || InputManager.GetShoot( 2 ) ) {
			NotificationCenter.PostNotification( Notification.QUIT );
		}
	}
}
