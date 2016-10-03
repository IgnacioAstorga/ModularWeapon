using UnityEngine;

public class OnCollisionBounce : ProjectileModifier {

	public bool preventRotation = true;
	public bool ignoreHitCollider = true;

	void OnCollisionEnter(Collision other) {
		_projectile.Velocity = Vector3.Reflect(_projectile.Velocity, other.contacts[0].normal);
		_rigidbody.velocity = _projectile.Velocity;
		if (preventRotation) {
			_rigidbody.angularVelocity = Vector3.zero;
			_transform.rotation = Quaternion.LookRotation(_projectile.Velocity);
		}
		if (ignoreHitCollider)
			_projectile.ignoreColliders.Add(other.collider);
	}
}