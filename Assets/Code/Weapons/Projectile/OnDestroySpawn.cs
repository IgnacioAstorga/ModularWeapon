using UnityEngine;

public class OnDestroySpawn : MonoBehaviour {

	public GameObject spawnedPrefab;
	public bool inheritSize;

	private WeaponProjectile _projectile;
	private Transform _transform;

	private bool _isQuitting;

	void Awake() {
		_projectile = GetComponent<WeaponProjectile>();
		_transform = transform;
	}

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
			spawned.transform.localScale *= _projectile.GetSize();
	}
}