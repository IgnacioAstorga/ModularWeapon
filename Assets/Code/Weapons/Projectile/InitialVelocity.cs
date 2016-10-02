using UnityEngine;

[RequireComponent(typeof(WeaponProjectile))]
[RequireComponent(typeof(Rigidbody))]
public class InitialVelocity : MonoBehaviour, SimulateComponent {

	public Vector3 initialVelocity = Vector3.forward;
	public Vector3 dispersion = Vector3.zero;
	public bool inheritVelocity = false;

	private Vector3 _velocity;

	private WeaponProjectile _projectile;
	private Rigidbody _rigidbody;
	private Transform _transform;

	void Awake() {
		_projectile = GetComponent<WeaponProjectile>();
		_rigidbody = GetComponent<Rigidbody>();
		_transform = transform;
	}

	void Start() {
		_rigidbody.AddForce(CalculateVelocity(), ForceMode.VelocityChange);
	}

	public void Simulate(float timeToSimulate) {
		_rigidbody.MovePosition(_transform.position + CalculateVelocity() * timeToSimulate);
	}

	private Vector3 CalculateVelocity() {
		if (_velocity == Vector3.zero) {
			_velocity = initialVelocity;
			_velocity *= _projectile.Modifiers.speedMultiplier;

			Quaternion weaponDeviation = Quaternion.FromToRotation(Vector3.forward, _projectile.Modifiers.baseDirection);
			_velocity = weaponDeviation * _velocity;

			Vector3 dispersion = this.dispersion + _projectile.Modifiers.additionalDispersion;
			dispersion.x = Random.Range(-dispersion.x, dispersion.x);
			dispersion.y = Random.Range(-dispersion.y, dispersion.y);
			dispersion.z = Random.Range(-dispersion.z, dispersion.z);
			Quaternion dispersionDeviation = Quaternion.Euler(dispersion);
			_velocity = dispersionDeviation * _velocity;

			_velocity = _transform.TransformDirection(_velocity);

			if (inheritVelocity)
				_velocity += _projectile.Weapon.Character.Velocity;
		}

		return _velocity;
	}
}