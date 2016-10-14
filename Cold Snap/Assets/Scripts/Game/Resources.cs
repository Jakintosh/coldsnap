using UnityEngine;

public class Resources : MonoBehaviour {

	// *************** Singleton implementation ***************

	private static Resources _instance;
	public static Resources Instance {
		get {
			return _instance;
		}
	}

	public void Init () {
		
		_instance = FindObjectOfType<Resources>();
	}

	// ********************************************************

	[System.Serializable] public struct Environment_Resources {

		[System.Serializable] public struct Tile_Resources {
			[SerializeField] public GameObject Default;
			[SerializeField] public GameObject Invalid;			
		}

		[SerializeField] public Tile_Resources Tiles;
	}

	// fields
	public Environment_Resources Environment;
}
