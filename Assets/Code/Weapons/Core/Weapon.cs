using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {

	public int SectionCount { get { return sections.Length; } }
	public List<WeaponProjectile> Projectiles { get; set; }

	public WeaponSection[] sections;
	public Transform[] firePoints;

	private int _nextFirePoint;

	void Awake() {
		foreach (WeaponSection section in sections)
			section.AssignWeapon(this);

		_nextFirePoint = 0;
	}

	void Update() {
		ReadInput();
	}

	private void ReadInput() {
		if (Input.GetButton("Fire1"))
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

	public Transform GetFirePoint() {
		if (_nextFirePoint >= firePoints.Length)
			_nextFirePoint = 0;
		return firePoints[_nextFirePoint++];
	}
}
