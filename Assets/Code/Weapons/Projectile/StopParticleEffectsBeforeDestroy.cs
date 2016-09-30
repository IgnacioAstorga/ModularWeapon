using UnityEngine;

[RequireComponent(typeof(LifetimeDestroy))]
public class StopParticleEffectsBeforeDestroy : MonoBehaviour {

	private LifetimeDestroy _lifetime;
	private ParticleSystem[] _systems;

	void Awake() {
		_lifetime = GetComponent<LifetimeDestroy>();
		_systems = GetComponentsInChildren<ParticleSystem>();
	}

	void Update() {
		float remainingTime = _lifetime.GetRemainingTime();
		foreach (ParticleSystem system in _systems) {
			if (remainingTime <= system.duration)
				system.Stop();
		}
	}
}
