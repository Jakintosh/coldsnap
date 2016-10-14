using System.Collections.Generic;
using UnityEngine;

public class LevelLoader {

	public void LoadDefaultLevel () {

		string defaultLayout = 
			"0000000000000000000000000" +
			"0                       0" +
			"0                       0" +
			"0    000000000000000    0" +
			"0    0             0    0" +
			"0    0             0    0" +
			"0    0             0    0" +
			"0    0             0    0" +
			"0                       0" +
			"0                       0" +
			"0                       0" +
			"0                       0" +
			"0                       0" +
			"0                       0" +
			"0000000000000000000000000";

		LoadLevel( defaultLayout );
	}

	public void LoadLevel ( string layout ) {

		int pos = -1;
		foreach ( char c in layout ) {

			pos++;

			if ( c == ' ' ) {
				continue;
			}

			var x = pos % SCREEN_W;
			var y = pos / SCREEN_W;
			if ( _tileSymbols.ContainsKey( c ) ) {
				SpawnTile( _tileSymbols[c], new Vector2( x, y ) );
			} else {
				Debug.LogError( "Tried to spawn a tile with an invalid symbol." );
				SpawnTile( TileType.INVALID, new Vector2( x, y ) );
			}
		}
	}

	// ********** Tile Loading **********

	private const int SCREEN_W = 25;
	private const int SCREEN_H = 15;

	// define tiles
	private enum TileType {
		INVALID = -1,
		DEFAULT,
		NUM_TILES
	}

	// map chars to tiles
	private Dictionary<char,TileType> _tileSymbols = new Dictionary<char, TileType> () {
		{ '0', TileType.DEFAULT }
	};

	// map tiles to prefabs
	private Dictionary<TileType, GameObject> _tilePrefabs = new Dictionary<TileType, GameObject> () {
		{ TileType.DEFAULT, Resources.Instance.Environment.Tiles.Default },
		{ TileType.INVALID, Resources.Instance.Environment.Tiles.Invalid }
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
		var pos = new Vector3( x, y );

		Object.Instantiate( prefab, pos, Quaternion.identity );
	}
}
