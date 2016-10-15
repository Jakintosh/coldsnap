using UnityEngine;

public class View : MonoBehaviour {

	[Header("Folders")]
	[SerializeField] private Transform _cameras;
	[SerializeField] private Transform _environment;

	[Header("Managers")]
	[SerializeField] private UIManager _ui;


	public UIManager UI {
		get {
			return _ui;
		}
	}
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
