using UnityEngine;

public class ProjectileParticle : MonoBehaviour {

	private void Start () {
		Destroy( gameObject, 1 );
	}
}
