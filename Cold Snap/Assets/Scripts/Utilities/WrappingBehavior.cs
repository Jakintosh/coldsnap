using UnityEngine;
using System.Collections;

public class WrappingBehavior : MonoBehaviour {

	void Update () {

		// adjust for wrapping
		float newX = transform.position.x;
		float newY = transform.position.y;
		if ( transform.position.x > 25.5f ) {
			newX -= 25f;
		} else if ( transform.position.x < -0.5f ) {
			newX += 25f;
		}
		if ( transform.position.y > 15.5f ) {
			newY -= 15f;
		} else if ( transform.position.y < -0.5f ) {
			newY += 15f;
		}
			
		transform.position = new Vector3( newX, newY );
	}
}
