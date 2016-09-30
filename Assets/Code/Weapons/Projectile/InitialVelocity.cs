using UnityEngine;

[RequireComponent(typeof(WeaponProjectile))]
[RequireComponent(typeof(Rigidbody))]
public class InitialVelocity : MonoBehaviour, SimulateComponent {

	public Vector3 Velocity {
		get { return _rigidbody.velocity; }
		set { _rigidbody.velocity = value; }
	}

	public Vector3 initialVelocity = Vector3.forward;
	public float dispersion = 0;

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
		_transform.Translate(CalculateVelocity() * timeToSimulate);
	}

	private Vector3 CalculateVelocity() {
		if (Velocity == Vector3.zero) {
			Velocity = _transform.TransformDirection(initialVelocity);
			Velocity *= _projectile.Modifiers.speedMultiplier;

			Quaternion weaponDeviation = Quaternion.FromToRotation(Vector3.forward, _projectile.Modifiers.baseDirection);
			Velocity = weaponDeviation * Velocity;

			Vector3 dispersion = _projectile.Modifiers.additionalDispersion;
			dispersion.x = Random.Range(-dispersion.x, dispersion.x);
			dispersion.y = Random.Range(-dispersion.y, dispersion.y);
			dispersion.z = Random.Range(-dispersion.z, dispersion.z);
			Quaternion dispersionDeviation = Quaternion.Euler(dispersion);
			Velocity = dispersionDeviation * Velocity;
		}

		return Velocity;
	}
}