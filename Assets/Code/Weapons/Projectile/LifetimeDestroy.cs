using UnityEngine;

public class LifetimeDestroy : ProjectileModifier {

	public float durationFactor = 1;

	void Start() {
		_projectile.LifeTime = 0;
	}

	void Update() {
		_projectile.Duration = _projectile.Parameters.duration * durationFactor;
		_projectile.LifeTime += Time.deltaTime;
		if (_projectile.LifeTime >= _projectile.Duration)
			Destroy(gameObject);
	}

	public override void Simulate(float timeToSimulate) {
		_projectile.LifeTime += timeToSimulate;
	}
}