using UnityEngine;
using System.Collections.Generic;

public class AutomaticWeaponModule : WeaponModule {

	public float fireRate;

	private float _timeForNextShoot;

	public override WeaponProjectile[] OnPressFire() {
		_timeForNextShoot = 0;
		return AutomaticFire();
	}

	public override WeaponProjectile[] OnHoldFire() {
		_timeForNextShoot -= Time.deltaTime;
		return AutomaticFire();
	}

	public override WeaponProjectile[] OnReleaseFire() {
		// Do nothing
		return null;
	}

	protected WeaponProjectile[] AutomaticFire() {
		float fireDelay = 1f / fireRate;
		List<WeaponProjectile> projectiles = new List<WeaponProjectile>();
		while (_timeForNextShoot <= 0) {
			projectiles.Add(FireProjectile(-_timeForNextShoot));
			_timeForNextShoot += fireDelay;
		}
		return projectiles.ToArray();
	}
}
