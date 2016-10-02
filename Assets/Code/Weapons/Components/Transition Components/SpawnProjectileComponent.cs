using UnityEngine;
using System.Collections.Generic;

public class SpawnProjectileComponent : TransitionComponent {
	
	public int numberOfProjectiles = 1;
	public bool evenDistribution = false;

	public override WeaponProjectile[] OnTransition(WeaponProjectile projectile) {
		Transform projectileTransform = projectile.transform;
		
		List<WeaponProjectile> projectiles = new List<WeaponProjectile>();
		for (int i = 0; i < numberOfProjectiles; i++) {
			WeaponProjectile newProjectile = Module.FireProjectile(projectileTransform.position, projectileTransform.rotation, transitionModifiers);
			newProjectile.Modifiers *= projectile.Modifiers;
			if (evenDistribution) {
				Vector3 projectileAngle = Vector3.Lerp(-newProjectile.Modifiers.additionalDispersion, newProjectile.Modifiers.additionalDispersion, i / (float)(numberOfProjectiles - 1));
				newProjectile.Modifiers.additionalDispersion = Vector3.zero;
				newProjectile.Modifiers.baseDirection = Quaternion.Euler(projectileAngle) * newProjectile.Modifiers.baseDirection;
			}
			projectiles.Add(newProjectile);
		}

		return projectiles.ToArray();
	}
}