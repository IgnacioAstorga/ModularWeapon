using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {

	public int SectionCount { get { return sections.Length; } }

	public WeaponSection[] sections;
	public Transform firePoint;

	public List<WeaponProjectile> Projectiles { get; set; }

	void Awake() {
		foreach (WeaponSection section in sections) {
			section.Weapon = this;
			section.Awake();
		}
	}

	void Start() {
		foreach (WeaponSection section in sections)
			section.Start();
	}

	void Update() {
		ReadInput();
		UpdateSections();
	}

	private void ReadInput() {
		if (Input.GetButton("Fire"))
			Fire();
	}

	public void Fire() {
		WeaponProjectile[] projectilesFired = sections[0].transitionModule.Fire();
		foreach (WeaponProjectile projectile in projectilesFired)
			RegisterProjectile(projectile);
	}

	private void RegisterProjectile(WeaponProjectile projectile) {
		Projectiles.Add(projectile);
		projectile.Weapon = this;
	}

	private void UpdateSections() {
		foreach (WeaponSection section in sections)
			section.Update();
	}

	public Transform GetFirePoint() {
		return firePoint;
	}
}
