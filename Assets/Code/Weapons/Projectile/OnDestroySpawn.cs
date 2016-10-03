using UnityEngine;

public class OnDestroySpawn : ProjectileModifier {

	public GameObject spawnedPrefab;
	public bool inheritSize;

	private bool _isQuitting;

	void Start() {
		_isQuitting = false;
	}

	void OnApplicationQuit() {
		_isQuitting = true;
	}

	void OnDestroy() {
		if (_isQuitting)
			return;

		GameObject spawned = (GameObject)Instantiate(spawnedPrefab, _transform.position, _transform.rotation);
		if (inheritSize)
			spawned.transform.localScale *= _projectile.Size;
	}
}