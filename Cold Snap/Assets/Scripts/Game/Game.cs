using UnityEngine;

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

	private static Game _instance;
	public static Game Instance {
		get { return _instance; }
	}

	// ***************************************

	private LevelLoader _levelLoader;


	private void Awake () {

		_instance = this;

		_levelLoader = new LevelLoader ();

		_levelLoader.LoadDefaultLevel ();
		_levelLoader.SpawnPlayers ();
	}
}
