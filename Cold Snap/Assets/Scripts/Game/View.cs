using UnityEngine;

public class View : MonoBehaviour {

	[Header("Folders")]
	[SerializeField] private Transform _cameras;
	[SerializeField] private Transform _environment;

	[Header("Managers")]
	[SerializeField] private UIManager _ui;

	[Header("Particles")]
	[SerializeField] private GameObject _snowEffect;
	[SerializeField] private GameObject _portalEffect;

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

	public void SetSnowEnabled ( bool enabled ) {
		_snowEffect.SetActive( enabled );
	}
	public void SetPortalEnabled ( bool enabled ) {
		_portalEffect.SetActive( enabled );
	}
}
