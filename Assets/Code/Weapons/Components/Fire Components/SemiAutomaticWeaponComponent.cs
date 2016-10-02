using UnityEngine;
using System.Collections.Generic;

public class SemiAutomaticWeaponComponent : FireComponent {

	public float timeBetweenShots;
	public int numberOfProjetiles;
	public bool allowHoldFire;

	private float _timeSinceLastShot;

	void Update() {
		_timeSinceLastShot += Time.deltaTime;
	}

	public override WeaponProjectile[] OnPressFire() {
		if (_timeSinceLastShot >= timeBetweenShots) {
			_timeSinceLastShot = 0;
			return SemiAutomaticFire();
		}
		return null;
	}

	public override WeaponProjectile[] OnHoldFire() {
		if (allowHoldFire)
			return OnPressFire();
		return null;
	}

	public override WeaponProjectile[] OnReleaseFire() {
		// Do nothing
		return null;
	}

	protected WeaponProjectile[] SemiAutomaticFire() {
		List<WeaponProjectile> projectiles = new List<WeaponProjectile>();
		for (int i = 0; i < numberOfProjetiles; i++) {
			Transform firePoint = Module.WeaponSection.Weapon.GetFirePoint();
			projectiles.Add(Module.FireProjectile(firePoint.position, firePoint.rotation, fireModifiers));
		}
		return projectiles.ToArray();
	}
}
