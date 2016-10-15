using UnityEngine;

public class View : MonoBehaviour {

	[SerializeField] private Transform _cameras;
	[SerializeField] private Transform _environment;


	public Transform Environment {
		get {
			return _environment;
		}
	}

	public Transform Cameras {
		get {
			return _cameras;
		}
	}
}
