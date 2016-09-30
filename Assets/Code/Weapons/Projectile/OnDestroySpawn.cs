using UnityEngine;

public class OnDestroySpawn : MonoBehaviour {

	public GameObject spawnedPrefab;

	private Transform _transform;

	private bool _isQuitting;

	void Awake() {
		_transform = transform;
	}

	void Start() {
		_isQuitting = false;
	}

	void OnApplicationQuit() {
		_isQuitting = true;
	}

	void OnDestroy() {
		if (!_isQuitting)
			Instantiate(spawnedPrefab, _transform.position, _transform.rotation);
	}
}