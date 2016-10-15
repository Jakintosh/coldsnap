using UnityEngine;

public enum Direction {
	LEFT,
	RIGHT,
	NONE
}

public struct Frame {

	private Vector2 _position;
	private float _width;
	private float _height;

	public Frame ( float width, float height ) {

		_position = Vector2.zero;
		_width = width;
		_height = height;
	}

	public Vector2 Position {
		get { return _position; }
		set { _position = value; }
	}

	public Vector2 UL {
		get {
			return _position + new Vector2( -_width / 2,  _height / 2 );
		}
	}
	public Vector2 UC {
		get {
			return _position + new Vector2( 0f,  _height / 2 );
		}
	}
	public Vector2 UR {
		get {
			return _position + new Vector2(  _width / 2,  _height / 2 );
		}
	}
	public Vector2 MR {
		get {
			return _position + new Vector2( _width / 2, 0f );
		}
	}
	public Vector2 ML {
		get {
			return _position + new Vector2( -_width / 2, 0f );
		}
	}
	public Vector2 LL {
		get {
			return _position + new Vector2( -_width / 2, -_height / 2 );
		}
	}
	public Vector2 LC {
		get {
			return _position + new Vector2( 0f,  -_height / 2 );
		}
	}
	public Vector2 LR {
		get {
			return _position + new Vector2(  _width / 2, -_height / 2 );
		}
	}
}