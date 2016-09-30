using UnityEngine;

public class UIController : MonoBehaviour {

	public void Show() {
		gameObject.SetActive(true);
	}

	public void Hide() {
		gameObject.SetActive(false);
	}
}