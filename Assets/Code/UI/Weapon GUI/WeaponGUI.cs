using UnityEngine;

public class WeaponGUI : MonoBehaviour {
	
	public Transform sectionsObject;
	public WeaponSectionGUI sectionGUIPrefab;
	public WeaponModule[] availableModules;

	private Transform[] _sectionGUIs;

	void Awake() {
		GameController.weaponGUI = this;
	}

	void OnEnable() {
		Refresh();
	}

	public void Refresh() {
		if (GameController.weapon == null || GameController.weapon.sections[0].Weapon == null)
			return;

		if (_sectionGUIs != null) {
			foreach (Transform section in _sectionGUIs)
				Destroy(section.gameObject);
		}

		_sectionGUIs = new Transform[GameController.weapon.SectionCount];
		for (int i = 0; i < GameController.weapon.SectionCount; i++) {
			CreateSectionGUI(GameController.weapon.sections[i], i);
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