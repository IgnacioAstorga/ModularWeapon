using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class TransformSequence : Sequence<Transform> { }

public class Weapon : MonoBehaviour {

	public int SectionCount { get { return sections.Length; } }
	public List<WeaponProjectile> Projectiles { get; set; }

	public WeaponSection[] sections;
	public TransformSequence firePoints;

	void Awake() {
		Projectiles = new List<WeaponProjectile>();
		foreach (WeaponSection section in sections)
			section.AssignWeapon(this);
	}

	void Start() {
		firePoints.MoveNext();
	}

	void Update() {
		ReadInput();
	}

	private void ReadInput() {
		if (Input.GetButton("Fire1"))
			Fire();
		else
			Release();
	}

	public void Fire() {
		RegisterProjectile(sections[0].transitionModule.PressFire());
	}

	public void Release() {
		RegisterProjectile(sections[0].transitionModule.ReleaseFire());
	}

	private void RegisterProjectile(IEnumerable<WeaponProjectile> projectiles) {
		if (projectiles == null)
			return;

		foreach (WeaponProjectile projectile in projectiles)
			RegisterProjectile(projectile);
	}

	private void RegisterProjectile(WeaponProjectile projectile) {
		Projectiles.Add(projectile);
		projectile.Weapon = this;
	}

	public Transform GetFirePoint() {
		Transform point = firePoints.Current;
		firePoints.MoveNext();
		return point;
	}
}
