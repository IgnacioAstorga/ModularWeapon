using UnityEngine;

public class ProjectileOnVelocityComponent : TransitionComponent {

	public override void OnTransition(WeaponProjectile projectile) {
		Transform projectileTransform = projectile.transform;
		Module.FireProjectile(projectileTransform.position, projectileTransform.rotation);
	}
}