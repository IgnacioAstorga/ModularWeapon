public class ForceGravity : ProjectileModifier {

	public bool useGravity = true;

	void Start() {
		_projectile.Gravity = useGravity;
	}
}