using UnityEngine;

public class WeaponProjectile : MonoBehaviour {
	
	public Weapon Weapon { get; set; }

	private int _sectionIndex;

	private float _timeToSimulate;

	void Awake() {
		_sectionIndex = 0;
	}

	void FixedUpdate() {
		SimulateComponent[] simulateComponents = GetComponents<SimulateComponent>();
		while (_timeToSimulate > 0f) {
			_timeToSimulate -= Time.deltaTime;
			foreach (SimulateComponent simulateComponent in simulateComponents)
				simulateComponent.Simulate();
		}
	}

	public void Simulate(float time) {
		_timeToSimulate = time;
	}
}
