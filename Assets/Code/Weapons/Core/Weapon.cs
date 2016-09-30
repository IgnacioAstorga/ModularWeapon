using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class TransformSequence : Sequence<Transform> { }

public class Weapon : MonoBehaviour {

	public int SectionCount { get { return sections.Length; } }
	public List<WeaponProjectile> Projectiles { get; set; }
	public Transform ModuleParent { get { return CreateModuleParent(); } }

	public WeaponSection[] sections;
	public TransformSequence firePoints;

	private Transform _transform;

	private Transform _moduleParent;

	void Awake() {
		_transform = transform;

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
		RegisterProjectile(sections[0].TransitionModule.PressFire());
	}

	public void Release() {
		RegisterProjectile(sections[0].TransitionModule.ReleaseFire());
	}

	public void ClearProjectiles() {
		foreach (WeaponProjectile projectile in Projectiles)
			Destroy(projectile.gameObject);
		Projectiles.Clear();
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

	private Transform CreateModuleParent() {
		if (_moduleParent == null) {
			_moduleParent = new GameObject("Modules").transform;
			_moduleParent.parent = _transform;
		}
		return _moduleParent;
	}
}
