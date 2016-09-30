using UnityEngine;

[RequireComponent(typeof(WeaponProjectile))]
public class InitialSize : MonoBehaviour {

	public float size = 1;

	private WeaponProjectile _projectile;
	private Transform _transform;

	void Awake() {
		_projectile = GetComponent<WeaponProjectile>();
		_transform = transform;
	}

	void Start() {
		_transform.localScale *= size * _projectile.Modifiers.sizeMultiplier;
	}
}