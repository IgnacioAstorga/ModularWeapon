using UnityEngine;

public class StopParticleEffectsBeforeDestroy : ProjectileModifier {
	
	private ParticleSystem[] _systems;

	protected override void OnAwake() {
		_systems = GetComponentsInChildren<ParticleSystem>();
	}

	void Update() {
		float remainingTime = _projectile.Duration - _projectile.LifeTime;
		foreach (ParticleSystem system in _systems)
			if (remainingTime <= system.duration)
				system.Stop();
	}
}
