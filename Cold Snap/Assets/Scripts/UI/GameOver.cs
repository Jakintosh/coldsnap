using UnityEngine;

public class GameOver : MonoBehaviour {

	private void Update () {

		if ( InputManager.GetJump( 1 ) || InputManager.GetJump( 2 ) ) {
			NotificationCenter.PostNotification( Notification.REMATCH );
		}

		if ( InputManager.GetShoot( 1 ) || InputManager.GetShoot( 2 ) ) {
			NotificationCenter.PostNotification( Notification.QUIT );
		}
	}
}
