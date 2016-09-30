using UnityEngine;
using System;

[Serializable]
public class WeaponModuleModifiers {

	public float speedMultiplier = 1;
	public float durationMultiplier = 1;
	public float sizeMultiplier = 1;
	public Vector3 baseDirection = Vector3.forward;
	public Vector3 additionalDispersion = Vector3.zero;
}
