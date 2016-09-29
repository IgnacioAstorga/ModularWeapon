using UnityEngine;
using System;

[Serializable]
public class WeaponModule {

	public WeaponSection WeaponSection { get; set; }

	//------------- General -------------

	public void Start() {
		// Do nothing
	}

	public void Update() {
		UpdateTimers();
	}

	private void UpdateTimers() {
		_timeSinceLastFire += Time.deltaTime;
	}

	//------------- Transition -------------

	public float fireRate;

	private float _timeSinceLastFire;

	public WeaponProjectile[] Fire() {
		float fireDelay = 1f / fireRate;
		WeaponProjectile[] projectiles = new WeaponProjectile[(int)(_timeSinceLastFire / fireDelay)];
		int i = 0;
		while (_timeSinceLastFire >= fireDelay) {
			_timeSinceLastFire -= fireDelay;
			WeaponProjectile projectile = Shoot(_timeSinceLastFire);
			projectiles[i] = projectile;
			i++;
		}
		return projectiles;
	}

	public WeaponProjectile Shoot(float elapsedTime = 0f) {
		WeaponProjectile projectile = WeaponSection.projectileModule.CreateProjectile();
		if (elapsedTime > 0f)
			projectile.Simulate(elapsedTime);
		return projectile;
	}

	//------------- Projectile -------------

	public WeaponProjectile projectilePrefab;

	private WeaponProjectile CreateProjectile() {
		Transform firePoint = WeaponSection.Weapon.GetFirePoint();
		return (WeaponProjectile) GameObject.Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
	}
}
