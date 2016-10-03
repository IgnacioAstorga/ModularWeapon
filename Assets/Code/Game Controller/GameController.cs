using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField]
	private Weapon playerWeapon;

	void Awake() {
		Instance = this;
		weapon = playerWeapon;
	}

	public static GameController Instance { get; private set; }

	public static Weapon weapon;
	public static PauseGame pauseGame;
	public static CursorController cursorController;
	public static WeaponGUI weaponGUI;

	public static void AddSectionStatic() {
		Instance.AddSection();
	}

	public static void RemoveSectionStatic() {
		Instance.RemoveSection();
	}

	public void AddSection() {
		weapon.AddSection();
		weaponGUI.Refresh();
	}

	public void RemoveSection() {
		weapon.RemoveSection();
		weaponGUI.Refresh();
	}
}
