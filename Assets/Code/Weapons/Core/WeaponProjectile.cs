using UnityEngine;

public class WeaponProjectile : MonoBehaviour, SimulateComponent {

	public Weapon Weapon { get; set; }
	public WeaponModuleModifiers Modifiers { get; set; }
	
	private float _timeToSimulate;

	private SimulateComponent[] _simulateComponents;

	void Start() {
		_simulateComponents = GetComponents<SimulateComponent>();
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
}
