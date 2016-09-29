using UnityEngine;
using System;

[Serializable]
public class WeaponSection {

	public Weapon Weapon { get; set; }
	
	public WeaponModule transitionModule;
	public WeaponModule projectileModule;
	public WeaponModule[] upgradeModules;

	public void AssignWeapon(Weapon weapon) {
		Weapon = weapon;

		InstantiateModules();

		projectileModule.WeaponSection = this;
		transitionModule.WeaponSection = this;
		foreach (WeaponModule module in upgradeModules)
			module.WeaponSection = this;
	}
	
	private void InstantiateModules() {
		transitionModule = GameObject.Instantiate<WeaponModule>(transitionModule);
		transitionModule.transform.SetParent(Weapon.transform, false);

		projectileModule = GameObject.Instantiate<WeaponModule>(projectileModule);
		projectileModule.transform.SetParent(Weapon.transform, false);

		for (int i = 0; i < upgradeModules.Length; i++) {
			upgradeModules[i] = GameObject.Instantiate<WeaponModule>(upgradeModules[i]);
			upgradeModules[i].transform.SetParent(Weapon.transform, false);
		}
	}
}