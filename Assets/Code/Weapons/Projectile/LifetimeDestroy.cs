using UnityEngine;

[RequireComponent(typeof(WeaponProjectile))]
public class LifetimeDestroy : MonoBehaviour, SimulateComponent {

	public float duration = 1;

	private float _lifeTime;

	private WeaponProjectile _projectile;

	void Awake() {
		_projectile = GetComponent<WeaponProjectile>();
	}

	void Start() {
		_lifeTime = 0;
	}

	void Update() {
		_lifeTime += Time.deltaTime;
		if (_lifeTime >= GetTotalDuration())
			Destroy(gameObject);
	}

	public float GetTotalDuration() {
		return duration * _projectile.Modifiers.durationMultiplier;
	}

	public float GetRemainingTime() {
		return GetTotalDuration() - _lifeTime;
	}

	public void Simulate(float timeToSimulate) {
		_lifeTime += timeToSimulate;
	}
}