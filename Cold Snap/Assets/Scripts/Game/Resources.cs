using UnityEngine;

public class Resources : MonoBehaviour {

	[System.Serializable] public struct Environment_Resources {

		[System.Serializable] public struct Tile_Resources {

			[SerializeField] public GameObject Default;
			[SerializeField] public GameObject Invalid;

			[Space]
			[SerializeField] public GameObject IceBlock;	
			[SerializeField] public GameObject StoneBlock_0;	
			[SerializeField] public GameObject fillerBlock;

			[Space]
			[SerializeField] public GameObject GroundDirtSnow_0;
			[SerializeField] public GameObject GroundDirtSnow_1;	

			[Space]
			[SerializeField] public GameObject WallRight_0;
			[SerializeField] public GameObject WallRight_1;
			[SerializeField] public GameObject WallRight_2;

			[Space]
			[SerializeField] public GameObject WallLeft_0;
			[SerializeField] public GameObject WallLeft_1;
			[SerializeField] public GameObject WallLeft_2;

			[Space]
			[SerializeField] public GameObject Ceiling_0;
			[SerializeField] public GameObject Ceiling_1;
			[SerializeField] public GameObject Ceiling_2;

			[Space]
			[SerializeField] public GameObject ChainTop;
			[SerializeField] public GameObject ChainMid;
			[SerializeField] public GameObject ChainBottom;

			[Space]
			[SerializeField] public GameObject SnowTopLeft;
			[SerializeField] public GameObject SnowTopRight;
			[SerializeField] public GameObject Torch;
			[SerializeField] public GameObject SnowBump;
			[SerializeField] public GameObject SnowDriftLeft;
			[SerializeField] public GameObject SnowDriftRight;

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
