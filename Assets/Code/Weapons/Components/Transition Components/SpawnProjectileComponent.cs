using UnityEngine;
using System.Collections.Generic;

public class SpawnProjectileComponent : TransitionComponent {
	
	public int numberOfProjectiles = 1;
	public bool evenDistribution = false;

	public override WeaponProjectile[] OnTransition(WeaponProjectile projectile) {
		Transform projectileTransform = projectile.transform;
		
		List<WeaponProjectile> projectiles = new List<WeaponProjectile>();
		for (float i = 0; i < numberOfProjectiles; i++) {
			WeaponProjectile newProjectile = Module.FireProjectile(projectileTransform.position, projectileTransform.rotation, transitionParameters);
			if (evenDistribution) {
				Vector3 projectileAngle = Vector3.Lerp(-newProjectile.Parameters.dispersion, newProjectile.Parameters.dispersion, i / (numberOfProjectiles - 1));
				newProjectile.Parameters.dispersion = Vector3.zero;
				newProjectile.Parameters.velocity = Quaternion.Euler(projectileAngle) * newProjectile.Parameters.velocity;
			}
			newProjectile.IgnoreColliders(projectile.ignoreColliders);
			projectiles.Add(newProjectile);
		}

		return projectiles.ToArray();
	}
}