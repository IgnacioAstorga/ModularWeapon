using UnityEngine;

[RequireComponent(typeof(WeaponProjectile))]
[RequireComponent(typeof(Rigidbody))]
public class InitialAngularVelocity : MonoBehaviour, SimulateComponent {

	public Vector3 initialAngularVelocityMin = Vector3.left;
	public Vector3 initialAngularVelocityMax = Vector3.right;
	public float speedStrength = 0;

	private Vector3 _angularVelocity;

	private WeaponProjectile _projectile;
	private Rigidbody _rigidbody;
	private Transform _transform;

	void Awake() {
		_projectile = GetComponent<WeaponProjectile>();
		_rigidbody = GetComponent<Rigidbody>();
		_transform = transform;
	}

	void Start() {
		_rigidbody.angularVelocity = CalculateAngularVelocity();
	}

	public void Simulate(float timeToSimulate) {
		_transform.Rotate(CalculateAngularVelocity() * timeToSimulate);
	}

	private Vector3 CalculateAngularVelocity() {
		if (_angularVelocity == Vector3.zero) {
			_angularVelocity.x = Random.Range(-initialAngularVelocityMin.x, initialAngularVelocityMax.x);
			_angularVelocity.y = Random.Range(-initialAngularVelocityMin.y, initialAngularVelocityMax.y);
			_angularVelocity.z = Random.Range(-initialAngularVelocityMin.z, initialAngularVelocityMax.z);

			_angularVelocity *= Mathf.Pow(_projectile.Modifiers.speedMultiplier, speedStrength);

			_angularVelocity = _transform.TransformDirection(_angularVelocity);
		}

		return _angularVelocity;
	}
}