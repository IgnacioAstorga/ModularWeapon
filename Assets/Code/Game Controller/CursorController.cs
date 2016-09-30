using UnityEngine;

public class CursorController : MonoBehaviour {
	
	public bool Hidden { get; private set; }

	void Awake() {
		GameController.cursorController = this;
	}

	void Start() {
		ShowCursor(false);
	}

	void Update() {
		if (Input.GetButtonDown("Toggle Cursor"))
			ToggleCursor();
	}

	public void ToggleCursor() {
		ShowCursor(!Hidden);
	}

	public void ShowCursor(bool show) {
		if (show) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Hidden = false;
		}
		else {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Hidden = true;
		}
	}
}