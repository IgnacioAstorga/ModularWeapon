using UnityEngine;

[RequireComponent(typeof(WeaponModule))]
public abstract class TransitionComponent : MonoBehaviour {

	public WeaponModuleModifiers transitionModifiers;

	protected WeaponModule Module { get; private set; }

	void Awake() {
		Module = GetComponent<WeaponModule>();
	}

	public abstract WeaponProjectile[] OnTransition(WeaponProjectile projectile);
}