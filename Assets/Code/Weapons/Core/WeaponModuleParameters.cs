using UnityEngine;
using System;

[Serializable]
public class WeaponModuleParameters {

	public Vector3 velocity = Vector3.forward;
	public Vector3 dispersion = Vector3.zero;
	public float duration = 1;
	public float size = 1;
	public float damage = 1;
	public bool gravity = false;
	public float drag = 0;

	public WeaponModuleParameters() { }

	public WeaponModuleParameters(WeaponModuleParameters copy) : this() {
		velocity = copy.velocity;
		dispersion = copy.dispersion;
		duration = copy.duration;
		size = copy.size;
		damage = copy.damage;
		gravity = copy.gravity;
		drag = copy.drag;
	}
}
