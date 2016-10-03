using UnityEngine;

public class WeaponModule : MonoBehaviour {

	public WeaponSection WeaponSection { get; set; }

	protected bool IsPressed { get; set; }
	protected float TimePressed { get; set; }

	public string moduleName;
	public WeaponProjectile projectilePrefab;

	private FireComponent _fireComponent;
	private TransitionComponent _transitionComponent;

	void Awake() {
		_fireComponent = GetComponent<FireComponent>();
		if (_fireComponent == null)
			Debug.LogError("ERROR: No Fire Component attached to the object!");
		_transitionComponent = GetComponent<TransitionComponent>();
		if (_transitionComponent == null)
			Debug.LogError("ERROR: No Transition Component attached to the object!");
	}

	public string GetModuleName() {
		return moduleName;
	}

	public void PressFire() {
		if (IsPressed) {
			TimePressed += Time.deltaTime;
			WeaponSection.Weapon.RegisterProjectile(_fireComponent.OnHoldFire());
		}
		else {
			IsPressed = true;
			TimePressed = 0;
			WeaponSection.Weapon.RegisterProjectile(_fireComponent.OnPressFire());
		}
	}

	public void ReleaseFire() {
		if (!IsPressed)
			return;

		IsPressed = false;
		TimePressed = 0;
		WeaponSection.Weapon.RegisterProjectile(_fireComponent.OnReleaseFire());
	}

	public WeaponProjectile FireProjectile(Vector3 position, Quaternion rotation, WeaponModuleParameters parameters, float elapsedTime = 0f) {
		WeaponProjectile projectile = WeaponSection.ProjectileModule.CreateProjectile(position, rotation);
		projectile.SetParameters(parameters);
		projectile.NextSection = WeaponSection.NextSection;
		if (elapsedTime > 0f)
			projectile.Simulate(elapsedTime);
		return projectile;
	}

	private WeaponProjectile CreateProjectile(Vector3 position, Quaternion rotation) {
		return (WeaponProjectile) Instantiate(projectilePrefab, position, rotation);
	}

	public void StartTransition(WeaponProjectile projectile) {
		WeaponSection.Weapon.RegisterProjectile(_transitionComponent.OnTransition(projectile));
	}
}
