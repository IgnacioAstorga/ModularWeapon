using UnityEngine;

public class WeaponProjectile : MonoBehaviour, SimulateComponent {

	public Weapon Weapon { get; set; }
	public WeaponModuleModifiers Modifiers { get; set; }
	public WeaponSection NextSection { get; set; }

	public float size = 1;

	private float _timeToSimulate;
	private bool _isQuitting;

	private SimulateComponent[] _simulateComponents;
	private Transform _transform;

	void Awake() {
		_simulateComponents = GetComponents<SimulateComponent>();
		_transform = transform;
	}

	void Start() {
		_transform.localScale *= GetSize(); ;
	}

	void FixedUpdate() {
		if (_timeToSimulate > 0f) {
			foreach (SimulateComponent simulateComponent in _simulateComponents)
				simulateComponent.Simulate(_timeToSimulate);
			_timeToSimulate = 0f;
		}
	}

	public void Simulate(float timeToSimulate) {
		_timeToSimulate = timeToSimulate;
	}

	public float GetSize() {
		return size * Modifiers.sizeMultiplier;
	}

	void OnApplicationQuit() {
		_isQuitting = true;
	}

	void OnDestroy() {
		if (_isQuitting)
			return;

		if (NextSection != null)
			NextSection.TransitionModule.StartTransition(this);
	}
}
