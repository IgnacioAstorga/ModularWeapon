using UnityEngine;

public abstract class WeaponModule : MonoBehaviour {

	public WeaponSection WeaponSection { get; set; }

	protected bool IsPressed { get; set; }
	protected float TimePressed { get; set; }

	public WeaponProjectile projectilePrefab;

	public WeaponProjectile[] PressFire() {
		if (IsPressed) {
			TimePressed += Time.deltaTime;
			return OnHoldFire();
		}
		else {
			IsPressed = true;
			TimePressed = 0;
			return OnPressFire();
		}
	}

	public WeaponProjectile[] ReleaseFire() {
		if (!IsPressed)
			return null;

		IsPressed = false;
		TimePressed = 0;
		return OnReleaseFire();
	}

	public abstract WeaponProjectile[] OnPressFire();

	public abstract WeaponProjectile[] OnHoldFire();

	public abstract WeaponProjectile[] OnReleaseFire();

	public WeaponProjectile FireProjectile(float elapsedTime = 0f) {
		WeaponProjectile projectile = WeaponSection.projectileModule.CreateProjectile();
		if (elapsedTime > 0f)
			projectile.Simulate(elapsedTime);
		return projectile;
	}

	private WeaponProjectile CreateProjectile() {
		Transform firePoint = WeaponSection.Weapon.GetFirePoint();
		return (WeaponProjectile) Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
	}
}
