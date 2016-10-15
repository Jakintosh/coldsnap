using UnityEngine;
using System.Collections;

public class TextAnimation : MonoBehaviour {

	public float speed;
	public Vector2 minScale;
	public Vector2 maxScale;

	public float maxRotation;
	public float minRotation;


	private void Update () {

		float sinValue = Mathf.Abs( Mathf.Sin( Time.time * speed ) );
		float newX = minScale.x + ((maxScale.x - minScale.x)*sinValue);
		float newY = minScale.y + ((maxScale.y - minScale.y)*sinValue);

		float newRotation = minRotation + ((maxRotation - minRotation)*sinValue);
		gameObject.transform.localScale = new Vector3( newX, newY, 1);
		gameObject.transform.localEulerAngles =  new Vector3(0,0,newRotation); 
	}
}
