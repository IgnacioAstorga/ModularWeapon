﻿using UnityEngine;

[RequireComponent(typeof(WeaponModule))]
public abstract class FireComponent : MonoBehaviour {

	protected WeaponModule Module { get; private set; }

	void Awake() {
		Module = GetComponent<WeaponModule>();
	}
	
	public abstract WeaponProjectile[] OnPressFire();

	public abstract WeaponProjectile[] OnHoldFire();

	public abstract WeaponProjectile[] OnReleaseFire();
}