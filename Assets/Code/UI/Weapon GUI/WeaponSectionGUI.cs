using UnityEngine;
using UnityEngine.UI;

public class WeaponSectionGUI : MonoBehaviour {

	public Text sectionTitle;
	public Dropdown transitionDropdown;
	public Dropdown projectileDropdown;

	private WeaponModule[] _availableModules;
	private WeaponSection _section;

	public void SetSection(WeaponSection section, int index, WeaponModule[] availableModules) {
		_section = section;
		_availableModules = availableModules;

		sectionTitle.text = "Module " + index;

		transitionDropdown.ClearOptions();
		int selectedIndex = 0;
		for (int i = 0; i < _availableModules.Length; i++) {
			if (_availableModules[i].GetModuleName() == _section.TransitionModule.GetModuleName())
				selectedIndex = i;
			transitionDropdown.options.Add(new Dropdown.OptionData(_availableModules[i].GetModuleName()));
		}
		transitionDropdown.value = selectedIndex;

		projectileDropdown.ClearOptions();
		selectedIndex = 0;
		for (int i = 0; i < _availableModules.Length; i++) {
			if (_availableModules[i].GetType() == _section.ProjectileModule.GetType())
				selectedIndex = i;
			projectileDropdown.options.Add(new Dropdown.OptionData(_availableModules[i].GetModuleName()));
		}
		projectileDropdown.value = selectedIndex;
	}

	public void SetSelectedTransitionModule(int index) {
		_section.SetTransitionModule(_availableModules[index]);
	}

	public void SetSelectedProjectileModule(int index) {
		_section.SetProjectileModule(_availableModules[index]);
	}
}