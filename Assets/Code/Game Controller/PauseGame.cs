using UnityEngine;

public class PauseGame : MonoBehaviour {

	public bool Paused { get; private set; }

	public UIController UIController;

	void Awake() {
		GameController.pauseGame = this;
	}

	void Start() {
		UIController.Hide();
	}

	void Update() {
		if (Input.GetButtonDown("Pause"))
			TogglePause();
	}

	public void TogglePause() {
		Pause(!Paused);
	}

	public void Pause(bool paused) {
		Paused = paused;
		if (paused) {
			Time.timeScale = 0;
			UIController.Show();
			GameController.cursorController.ShowCursor(true);
		}
		else {
			Time.timeScale = 1;
			UIController.Hide();
			GameController.cursorController.ShowCursor(false);
		}
	}
}
