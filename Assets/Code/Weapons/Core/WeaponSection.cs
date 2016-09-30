using UnityEngine;
using System;

[Serializable]
public class WeaponSection {

	public Weapon Weapon { get; set; }
	
	public WeaponModule TransitionModule { get; set; }
	public WeaponModule ProjectileModule { get; set; }
	public WeaponModule[] UpgradeModules { get; set; }

	[SerializeField]
	private WeaponModule transitionModule;
	[SerializeField]
	private WeaponModule projectileModule;
	[SerializeField]
	private WeaponModule[] upgradeModules;

	public void AssignWeapon(Weapon weapon) {
		Weapon = weapon;
		SetTransitionModule(transitionModule);
		SetProjectileModule(projectileModule);
		SetUpgradeModules(upgradeModules);
	}

	public void SetTransitionModule(WeaponModule modulePrefab) {
		if (TransitionModule != null)
			GameObject.Destroy(TransitionModule);

		TransitionModule = GameObject.Instantiate<WeaponModule>(modulePrefab);
		TransitionModule.transform.SetParent(Weapon.ModuleParent, false);
		TransitionModule.WeaponSection = this;
	}

	public void SetProjectileModule(WeaponModule modulePrefab) {
		if (ProjectileModule != null)
			GameObject.Destroy(ProjectileModule);

		ProjectileModule = GameObject.Instantiate<WeaponModule>(modulePrefab);
		ProjectileModule.transform.SetParent(Weapon.ModuleParent, false);
		ProjectileModule.WeaponSection = this;
	}

	public void SetUpgradeModules(WeaponModule[] modulePrefabs) {
		if (UpgradeModules != null)
			foreach (WeaponModule upgradeModule in UpgradeModules)
				GameObject.Destroy(upgradeModule);

		UpgradeModules = new WeaponModule[modulePrefabs.Length];
		for (int i = 0; i < modulePrefabs.Length; i++) {
			UpgradeModules[i] = GameObject.Instantiate<WeaponModule>(modulePrefabs[i]);
			UpgradeModules[i].transform.SetParent(Weapon.ModuleParent, false);
			UpgradeModules[i].WeaponSection = this;
		}
	}
}