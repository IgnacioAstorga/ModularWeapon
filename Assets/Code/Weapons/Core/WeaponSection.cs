using System;

[Serializable]
public class WeaponSection {

	public Weapon Weapon { get; set; }

	[WeaponModuleTransition]
	public WeaponModule transitionModule;

	[WeaponModuleProjectile]
	public WeaponModule projectileModule;
	
	public WeaponModule[] upgradeModules;

	public void Awake() {
		projectileModule.WeaponSection = this;
		transitionModule.WeaponSection = this;
		foreach (WeaponModule module in upgradeModules)
			module.WeaponSection = this;
	}

	public void Start() {
		projectileModule.Start();
		transitionModule.Start();
		foreach (WeaponModule module in upgradeModules)
			module.Start();
	}

	public void Update() {
		projectileModule.Update();
		transitionModule.Update();
		foreach (WeaponModule module in upgradeModules)
			module.Update();
	}
}