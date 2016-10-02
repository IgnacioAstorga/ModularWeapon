using UnityEngine;

public class WeaponModule : MonoBehaviour {

	public WeaponSection WeaponSection { get; set; }

	protected bool IsPressed { get; set; }
	protected float TimePressed { get; set; }

	public string moduleName;
	public WeaponModuleModifiers transitionModifiers;
	public WeaponProjectile projectilePrefab;

	private FireComponent _fireComponent;
	private TransitionComponent _transitionComponent;

	void Awake() {
		_fireComponent = GetComponent<FireComponent>();
		if (_fireComponent == null)
			Debug.LogError("ERROR: No Fire Component attached to the object!");
		if (_transitionComponent == null)
			Debug.LogError("ERROR: No Transition Component attached to the object!");
	}

	public string GetModuleName() {
		return moduleName;
	}

	public WeaponProjectile[] PressFire() {
		if (IsPressed) {
			TimePressed += Time.deltaTime;
			return _fireComponent.OnHoldFire();
		}
		else {
			IsPressed = true;
			TimePressed = 0;
			return _fireComponent.OnPressFire();
		}
	}

	public WeaponProjectile[] ReleaseFire() {
		if (!IsPressed)
			return null;

		IsPressed = false;
		TimePressed = 0;
		return _fireComponent.OnReleaseFire();
	}

	public WeaponProjectile FireProjectile(Vector3 position, Quaternion rotation, float elapsedTime = 0f) {
		WeaponProjectile projectile = WeaponSection.ProjectileModule.CreateProjectile(position, rotation);
		projectile.Modifiers = transitionModifiers;
		projectile.NextSection = WeaponSection.NextSection;
		if (elapsedTime > 0f)
			projectile.Simulate(elapsedTime);
		return projectile;
	}

	private WeaponProjectile CreateProjectile(Vector3 position, Quaternion rotation) {
		return (WeaponProjectile) Instantiate(projectilePrefab, position, rotation);
	}

	public void StartTransition(WeaponProjectile projectile) {
		_transitionComponent.OnTransition(projectile);
	}
}
