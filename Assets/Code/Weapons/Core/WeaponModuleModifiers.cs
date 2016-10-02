using UnityEngine;
using System;

[Serializable]
public class WeaponModuleModifiers {

	public float speedMultiplier = 1;
	public float durationMultiplier = 1;
	public float sizeMultiplier = 1;
	public Vector3 additionalDispersion = Vector3.zero;
	public Vector3 baseDirection = Vector3.forward;

	public WeaponModuleModifiers() { }

	public WeaponModuleModifiers(WeaponModuleModifiers copy) : this() {
		speedMultiplier = copy.speedMultiplier;
		durationMultiplier = copy.durationMultiplier;
		sizeMultiplier = copy.sizeMultiplier;
		additionalDispersion = copy.additionalDispersion;
		baseDirection = copy.baseDirection;
	}

	public static WeaponModuleModifiers operator * (WeaponModuleModifiers mod1, WeaponModuleModifiers mod2) {
		WeaponModuleModifiers newModifiers = new WeaponModuleModifiers(mod1);

		newModifiers.speedMultiplier *= mod2.speedMultiplier;
		newModifiers.durationMultiplier *= mod2.durationMultiplier;
		newModifiers.sizeMultiplier *= mod2.sizeMultiplier;
		newModifiers.additionalDispersion += mod2.additionalDispersion;
		newModifiers.baseDirection = (newModifiers.baseDirection + mod2.baseDirection).normalized;

		return newModifiers;
	}
}
