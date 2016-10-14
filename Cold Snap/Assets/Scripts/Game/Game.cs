using UnityEngine;

public class Game : MonoBehaviour {

	private LevelLoader _levelLoader;

	[SerializeField] private Resources _resources;

	private void Awake () {

		_resources.Init ();
		_levelLoader = new LevelLoader ();

		_levelLoader.LoadDefaultLevel ();
	}
}
