using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	// ************** Singleton **************

	[SerializeField] private View _view;
	public static View View {
		get { return _instance._view; }
	}

	[SerializeField] private Resources _resources;
	public static Resources Resources {
		get { return _instance._resources; }
	}

	[SerializeField] private AudioSource _audioPlayer;
	public static AudioSource AudioPlayer {
		get { return _instance._audioPlayer; }
	}

	[SerializeField] private ScreenShake _screenShake;
	public static ScreenShake ScreenShake {
		get { return _instance._screenShake; }
	}

	private static Game _instance;
	public static Game Instance {
		get { return _instance; }
	}

	// ***************************************

	private LevelLoader _levelLoader;

	public bool player1Alive = true;
	public bool player2Alive = true;

	private void Awake () {

		_instance = this;
		_levelLoader = new LevelLoader ();

		// register for notifications
		NotificationCenter.RegisterForNotification( Notification.START_MENU_DISMISSED, PresentMatchSettings );
		NotificationCenter.RegisterForNotification( Notification.MATCH_SETTINGS_CONFIRMED, LoadLevel );
		NotificationCenter.RegisterForNotification( Notification.GAME_ENDED, GameEnded );
		NotificationCenter.RegisterForNotification( Notification.PLAYER1_DEAD, Player1Dead );
		NotificationCenter.RegisterForNotification( Notification.PLAYER2_DEAD, Player2Dead );
		NotificationCenter.RegisterForNotification( Notification.REMATCH, Rematch );
		NotificationCenter.RegisterForNotification( Notification.QUIT, Restart );

		// show start screen
		View.UI.ShowStartScreen ();
		View.SetSnowEnabled( true );
	}

	private void OnDestroy () {

		NotificationCenter.DeregisterForNotification( Notification.START_MENU_DISMISSED, PresentMatchSettings );
		NotificationCenter.DeregisterForNotification( Notification.MATCH_SETTINGS_CONFIRMED, LoadLevel );
		NotificationCenter.DeregisterForNotification( Notification.GAME_ENDED, GameEnded );
		NotificationCenter.DeregisterForNotification( Notification.PLAYER1_DEAD, Player1Dead );
		NotificationCenter.DeregisterForNotification( Notification.PLAYER2_DEAD, Player2Dead );
		NotificationCenter.DeregisterForNotification( Notification.REMATCH, Rematch );
		NotificationCenter.DeregisterForNotification( Notification.QUIT, Restart );
	}

	private void PresentMatchSettings () {

		View.UI.ShowMatchOptions ();
	}

	private void LoadLevel () {

		View.UI.DismissUI ();

		player1Alive = true;
		player2Alive = true;

		Game.AudioPlayer.clip = Game.Resources.Audio.Climb;
		Game.AudioPlayer.Play ();

		_levelLoader.LoadDefaultLevel ();
		_levelLoader.SpawnPlayers ();

		View.SetPortalEnabled( true );
	}

	private void Player1Dead () {

		player1Alive = false;
	}

	private void Player2Dead () {
		
		player2Alive = false;
	}

	private void Rematch () {

		AudioPlayer.Stop ();
		View.SetPortalEnabled( false );
		_levelLoader.ResetAll ();
		PresentMatchSettings ();
	}

	private void GameEnded () {

		View.UI.ShowGameOver ();
	}

	private void Restart () {

		SceneManager.LoadScene( 0 );
	}
}
