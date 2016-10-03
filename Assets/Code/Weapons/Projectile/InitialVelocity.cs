using UnityEngine;

public class InitialVelocity : ProjectileModifier {

	public float speedFactor = 1;
	public Vector3 additionalDispersion = Vector3.zero;
	public bool inheritVelocity = false;

	private bool _calculated = false;

	void Start() {
		_rigidbody.velocity = CalculateVelocity(); ;
	}

	public override void Simulate(float timeToSimulate) {
		// TODO: This causes problems
		//_rigidbody.MovePosition(_transform.position + CalculateVelocity() * timeToSimulate);
	}

	private Vector3 CalculateVelocity() {
		if (!_calculated) {
			_projectile.Velocity = _projectile.Parameters.velocity;
			_projectile.Velocity *= speedFactor;

			Vector3 dispersion = additionalDispersion + _projectile.Parameters.dispersion;
			dispersion.x = Random.Range(-dispersion.x, dispersion.x);
			dispersion.y = Random.Range(-dispersion.y, dispersion.y);
			dispersion.z = Random.Range(-dispersion.z, dispersion.z);
			Quaternion dispersionDeviation = Quaternion.Euler(dispersion);
			_projectile.Velocity = dispersionDeviation * _projectile.Velocity;

			_projectile.Velocity = _transform.TransformDirection(_projectile.Velocity);

			if (inheritVelocity)
				_projectile.Velocity += _projectile.Weapon.Character.Velocity;

			_calculated = true;
		}

		return _projectile.Velocity;
	}
}