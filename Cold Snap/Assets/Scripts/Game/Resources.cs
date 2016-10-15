using UnityEngine;

public class Resources : MonoBehaviour {

	[System.Serializable] public struct Environment_Resources {

		[System.Serializable] public struct Tile_Resources {
			[SerializeField] public GameObject Default;
			[SerializeField] public GameObject Invalid;			
		}

		[SerializeField] public Tile_Resources Tiles;
	}

	[System.Serializable] public struct Player_Resources {

		[SerializeField] public GameObject StandardPlayer;
	}

	[System.Serializable] public struct Projectile_Resources {

		[SerializeField] public GameObject Default;
	}



	// fields
	public Environment_Resources Environment;
	public Player_Resources Player;
	public Projectile_Resources Projectile;
}
