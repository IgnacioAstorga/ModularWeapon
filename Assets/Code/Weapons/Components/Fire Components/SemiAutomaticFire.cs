using UnityEngine;
using System.Collections.Generic;

public class SemiAutomaticFire : FireComponent {

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
			return Fire();
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

	protected WeaponProjectile[] Fire() {
		List<WeaponProjectile> projectiles = new List<WeaponProjectile>();
		for (int i = 0; i < numberOfProjetiles; i++) {
			Transform firePoint = Module.WeaponSection.Weapon.GetFirePoint();
			projectiles.Add(Module.FireProjectile(firePoint.position, firePoint.rotation, fireParameters));
		}
		return projectiles.ToArray();
	}
}
