using UnityEngine;

public class OnCollisionDestroy : ProjectileModifier {

	public LayerMask collisionLayer;

	void OnCollisionEnter(Collision other) {
		if (collisionLayer == (collisionLayer | (1 << other.gameObject.layer)))
			Destroy(gameObject);
	}
}