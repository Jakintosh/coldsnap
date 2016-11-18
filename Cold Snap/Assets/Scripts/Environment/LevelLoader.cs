using System.Collections.Generic;
using UnityEngine;

public class LevelLoader {

	private Vector2? _player1Start;
	private Vector2? _player2Start;

	private List<GameObject> allSpawnedObjects = new List<GameObject>();

	public void ResetAll () {
		
		foreach ( GameObject obj in allSpawnedObjects ) {
			Object.Destroy( obj );
		}
		allSpawnedObjects.Clear ();
	}
	public void LoadDefaultLevel () {

		string defaultLayout = 
			"r   SmSmnmmnmSnmnmnmS   e" +
			"f   S<  l--  - -l- >S   S" +
			"v   S               S   c" +
			"r   S               S   e" +
			"f   a               b   d" +
			"v                       c" +
			"S      ISIIIIIIISI      e" +
			"f       L       L       d" +
			"v       i       i       c" +
			"r       i       i       e" +
			"SSSS    T       T    SSSS" +
			"o  l    StyutyutS    l  p" +
			"        eFFFFFSFr        " +
			"        dSFFFFFFf        " +
			"S   StyuFFSFFFFFFtyuS   S";

		LoadLevel( defaultLayout );
	}
	public void LoadLevel ( string layout ) {

		int pos = -1;
		foreach ( char c in layout ) {

			pos++;

			// if space, move on
			if ( c == ' ' ) {
				continue;
			}

			// get tile position
			var x = pos % SCREEN_W;
			var y = pos / SCREEN_W;
			var tilePos = new Vector2( x, y );

			// if player spawn, move on
			if ( c == 'a' ) {
				_player1Start = tilePos;
				continue;
			} if ( c == 'b' ) {
				_player2Start = tilePos;
				continue;
			}

			// spawn tile
			if ( _tileSymbols.ContainsKey( c ) ) {
				SpawnTile( _tileSymbols[c], tilePos );
			} else {
				Debug.LogError( "Tried to spawn a tile with an invalid symbol." );
				SpawnTile( TileType.INVALID, tilePos );
			}
		}
	}
	public void SpawnPlayers () {

		if ( _player1Start.HasValue ) {

			var prefab = Game.Resources.Player.StandardPlayer;
			var startPos = _player1Start.Value + new Vector2( 0.5f, 1f );
			var playerGO = Object.Instantiate( prefab, startPos, Quaternion.identity ) as GameObject;
			playerGO.transform.SetParent( Game.View.Environment );

			var player = playerGO.GetComponent<Player>();
			player.PlayerNumber = 1;
			player.Heading = Direction.RIGHT;
			player.Animator = Game.Resources.Player.Animator.Red;

			allSpawnedObjects.Add( playerGO );
		}

		if ( _player2Start.HasValue ) {

			var prefab = Game.Resources.Player.StandardPlayer;
			var startPos = _player2Start.Value + new Vector2( 0.5f, 1f );
			var playerGO = Object.Instantiate( prefab, startPos, Quaternion.identity ) as GameObject;
			playerGO.transform.SetParent( Game.View.Environment );

			var player = playerGO.GetComponent<Player>();
			player.PlayerNumber = 2;
			player.Heading = Direction.LEFT;
			player.Animator = Game.Resources.Player.Animator.Yellow;

			allSpawnedObjects.Add( playerGO );
		}
	}

	// ****************** Tile Loading ******************

	private const int SCREEN_W = 25;
	private const int SCREEN_H = 15;

	// define tiles
	private enum TileType {
		INVALID = -1,
		DEFAULT,

		ICEBLOCK,
		STONEBLOCK_0,
		FILLERBLOCK,

		GROUNDDIRTSNOW_0,
		GROUNDDIRTSNOW_1,

		CEILING_0,
		CEILING_1,
		CEILING_2,

		WALLLEFT_0,
		WALLLEFT_1,
		WALLLEFT_2,

		WALLRIGHT_0,
		WALLRIGHT_1,
		WALLRIGHT_2,

		CHAINTOP,
		CHAINMID,
		CHAINBOTTOM,

		SNOWTOPLEFT,
		SNOWTOPRIGHT,
		TORCH,
		SNOWBUMP,
		SNOWDRIFTLEFT,
		SNOWDRIFTRIGHT,

		NUM_TILES
	}

	// map chars to tiles
	private Dictionary<char,TileType> _tileSymbols = new Dictionary<char, TileType> () {
		{ '0', TileType.DEFAULT },

		{ 'I', TileType.ICEBLOCK },
		{ 'S', TileType.STONEBLOCK_0 },
		{ 'F', TileType.FILLERBLOCK },

		{ 'n', TileType.GROUNDDIRTSNOW_0 },
		{ 'm', TileType.GROUNDDIRTSNOW_1 },

		{ 't', TileType.CEILING_0 },
		{ 'y', TileType.CEILING_1 },
		{ 'u', TileType.CEILING_2 },

		{ 'r', TileType.WALLRIGHT_0 },
		{ 'f', TileType.WALLRIGHT_1 },
		{ 'v', TileType.WALLRIGHT_2 },

		{ 'e', TileType.WALLLEFT_0 },
		{ 'd', TileType.WALLLEFT_1 },
		{ 'c', TileType.WALLLEFT_2 },

		{ 'T', TileType.CHAINTOP },
		{ 'i', TileType.CHAINMID },
		{ 'L', TileType.CHAINBOTTOM },

		{ 'o', TileType.SNOWTOPLEFT },
		{ 'p', TileType.SNOWTOPRIGHT },
		{ 'l', TileType.TORCH },
		{ '-', TileType.SNOWBUMP },
		{ '<', TileType.SNOWDRIFTLEFT },
		{ '>', TileType.SNOWDRIFTRIGHT }
	};

	// map tiles to prefabs
	private Dictionary<TileType, GameObject> _tilePrefabs = new Dictionary<TileType, GameObject> () {
		{ TileType.DEFAULT, Game.Resources.Environment.Tiles.Default },
		{ TileType.INVALID, Game.Resources.Environment.Tiles.Invalid },

		{ TileType.STONEBLOCK_0, Game.Resources.Environment.Tiles.StoneBlock_0 },
		{ TileType.ICEBLOCK, Game.Resources.Environment.Tiles.IceBlock },
		{ TileType.FILLERBLOCK, Game.Resources.Environment.Tiles.fillerBlock },

		{ TileType.GROUNDDIRTSNOW_0, Game.Resources.Environment.Tiles.GroundDirtSnow_0 },
		{ TileType.GROUNDDIRTSNOW_1, Game.Resources.Environment.Tiles.GroundDirtSnow_1 },

		{ TileType.CEILING_0, Game.Resources.Environment.Tiles.Ceiling_0 },
		{ TileType.CEILING_1, Game.Resources.Environment.Tiles.Ceiling_1 },
		{ TileType.CEILING_2, Game.Resources.Environment.Tiles.Ceiling_2 },

		{ TileType.WALLRIGHT_0, Game.Resources.Environment.Tiles.WallRight_0 },
		{ TileType.WALLRIGHT_1, Game.Resources.Environment.Tiles.WallRight_1 },
		{ TileType.WALLRIGHT_2, Game.Resources.Environment.Tiles.WallRight_2 },

		{ TileType.WALLLEFT_0, Game.Resources.Environment.Tiles.WallLeft_0 },
		{ TileType.WALLLEFT_1, Game.Resources.Environment.Tiles.WallLeft_1 },
		{ TileType.WALLLEFT_2, Game.Resources.Environment.Tiles.WallLeft_2 },

		{ TileType.CHAINTOP, Game.Resources.Environment.Tiles.ChainTop },
		{ TileType.CHAINMID, Game.Resources.Environment.Tiles.ChainMid },
		{ TileType.CHAINBOTTOM, Game.Resources.Environment.Tiles.ChainBottom },

		{ TileType.SNOWTOPLEFT, Game.Resources.Environment.Tiles.SnowTopLeft },
		{ TileType.SNOWTOPRIGHT, Game.Resources.Environment.Tiles.SnowTopRight },
		{ TileType.TORCH, Game.Resources.Environment.Tiles.Torch },
		{ TileType.SNOWBUMP, Game.Resources.Environment.Tiles.SnowBump },
		{ TileType.SNOWDRIFTLEFT, Game.Resources.Environment.Tiles.SnowDriftLeft },
		{ TileType.SNOWDRIFTRIGHT, Game.Resources.Environment.Tiles.SnowDriftRight }
	};

	private void SpawnTile ( TileType type, Vector2 position ) {

		// get prefab
		GameObject prefab = null;
		if ( _tilePrefabs.ContainsKey( type ) ) {
			prefab = _tilePrefabs[type];
		} else {
			Debug.LogWarning( "Prefab not linked for tile type; couldn't spawn tile." );
			return;
		}

		// get position
		var x = position.x + 0.5f;
		var y = position.y + 0.5f;
		var pos = new Vector3( x, y, prefab.transform.position.z );

		var tileGO = Object.Instantiate( prefab, pos, Quaternion.identity ) as GameObject;
		tileGO.transform.SetParent( Game.View.Environment );

		allSpawnedObjects.Add( tileGO );
	}

	// **************************************************
}
