using UnityEngine;

public class WeaponGUI : MonoBehaviour {

	public Weapon weapon;
	public Transform sectionsObject;
	public WeaponSectionGUI sectionGUIPrefab;
	public WeaponModule[] availableModules;

	private Transform[] _sectionGUIs;

	void OnEnable() {
		if (_sectionGUIs == null) {
			// First call when the game begins
			_sectionGUIs = new Transform[weapon.SectionCount];
			return;
		}

		foreach (Transform section in _sectionGUIs)
				Destroy(section);

		for (int i = 0; i < weapon.SectionCount; i++) {
			CreateSectionGUI(weapon.sections[i], i);
		}
	}

	private void CreateSectionGUI(WeaponSection section, int index) {
		WeaponSectionGUI sectionGUI = Instantiate(sectionGUIPrefab);
		_sectionGUIs[index] = sectionGUI.transform;
		_sectionGUIs[index].SetParent(sectionsObject, false);
		_sectionGUIs[index].SetAsLastSibling();
		sectionGUI.SetSection(section, index, availableModules);
	}
}