using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	[SerializeField] private GameObject _startScreen;
	[SerializeField] private GameObject _matchOptions;

	private GameObject _activeScreen;

	public void ShowStartScreen () {

		DismissUI ();
		_activeScreen = _startScreen;
		_activeScreen.SetActive( true );

		// run start screen coroutine
		StartCoroutine( StartScreenCoroutine() );
	}

	public void ShowMatchOptions () {

		DismissUI ();
		_activeScreen = _matchOptions;
		_activeScreen.SetActive( true );
	}

	public void DismissUI () {

		if ( _activeScreen != null ) {
			_activeScreen.SetActive( false );
			_activeScreen = null;
		}
	}

	public IEnumerator StartScreenCoroutine () {
		yield return new WaitUntil ( () => Input.anyKeyDown );
		Debug.Log("Test");
		NotificationCenter.PostNotification( Notification.START_MENU_DISMISSED );
	}
}
