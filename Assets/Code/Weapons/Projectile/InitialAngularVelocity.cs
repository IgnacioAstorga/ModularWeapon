using UnityEngine;

public class InitialAngularVelocity : ProjectileModifier {

	public Vector3 initialAngularVelocityMin = Vector3.left;
	public Vector3 initialAngularVelocityMax = Vector3.right;

	private Vector3 _angularVelocity;

	void Start() {
		_rigidbody.angularVelocity = CalculateAngularVelocity();
	}

	public override void Simulate(float timeToSimulate) {
		_transform.Rotate(CalculateAngularVelocity() * timeToSimulate);
	}

	private Vector3 CalculateAngularVelocity() {
		if (_angularVelocity == Vector3.zero) {
			_angularVelocity.x = Random.Range(-initialAngularVelocityMin.x, initialAngularVelocityMax.x);
			_angularVelocity.y = Random.Range(-initialAngularVelocityMin.y, initialAngularVelocityMax.y);
			_angularVelocity.z = Random.Range(-initialAngularVelocityMin.z, initialAngularVelocityMax.z);
			_angularVelocity = _transform.TransformDirection(_angularVelocity);
		}

		return _angularVelocity;
	}
}