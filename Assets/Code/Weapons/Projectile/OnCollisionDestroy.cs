using UnityEngine;

[RequireComponent(typeof(WeaponProjectile))]
public class OnCollisionDestroy : MonoBehaviour {

	public LayerMask collisionLayer;

	void OnCollisionEnter(Collision other) {
		if (collisionLayer == (collisionLayer | (1 << other.gameObject.layer)))
			Destroy(gameObject);
	}
}