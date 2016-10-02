﻿using UnityEngine;
using System.Collections.Generic;

public class AutomaticWeaponComponent : FireComponent {

	public float fireRate;

	private float _timeForNextShoot;

	void Update() {
		_timeForNextShoot -= Time.deltaTime;
	}

	public override WeaponProjectile[] OnPressFire() {
		if (_timeForNextShoot <= 0)
			_timeForNextShoot = 0;
		return AutomaticFire();
	}

	public override WeaponProjectile[] OnHoldFire() {
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
			Transform firePoint = Module.WeaponSection.Weapon.GetFirePoint();
			projectiles.Add(Module.FireProjectile(firePoint .position, firePoint .rotation, fireModifiers, - _timeForNextShoot));
			_timeForNextShoot += fireDelay;
		}
		return projectiles.ToArray();
	}
}
