using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class WeaponProjectile : ProjectileModifier {

	public Weapon Weapon { get; set; }
	public WeaponModuleParameters Parameters { get; private set; }
	public WeaponSection NextSection { get; set; }

	public Vector3 Velocity { get; set; }
	public Vector3 Dispersion { get; set; }
	public float LifeTime { get; set; }
	public float Duration { get; set; }
	public float Size { get; set; }
	public float Damage { get; set; }
	public bool Gravity { get; set; }
	public float Drag { get; set; }

	[HideInInspector]
	public float timeToIgnoreCollisions = 0.1f;
	[HideInInspector]
	public List<Collider> ignoreColliders = new List<Collider>();

	private Vector3 _originalScale;
	private float _timeToSimulate;
	private bool _isQuitting;

	private ProjectileModifier[] _modifiers;
	private Collider[] _bulletColliders;

	public void SetParameters(WeaponModuleParameters parameters) {
		Parameters = new WeaponModuleParameters(parameters);
		Velocity = Parameters.velocity;
		Dispersion = Parameters.dispersion;
		Duration = Parameters.duration;
		Size = Parameters.size;
		Damage = Parameters.damage;
		Gravity = Parameters.gravity;
		Drag = Parameters.drag;
		LifeTime = 0;
	}

	protected override void OnAwake() {
		_bulletColliders = GetComponentsInChildren<Collider>();
	}

	void Start() {
		_originalScale = _transform.localScale;
		_transform.localScale = _originalScale * Size;
		_modifiers = GetComponents<ProjectileModifier>();
		
		IgnoreColliders(Weapon.Character.GetComponents<Collider>());
	}

	void FixedUpdate() {
		if (_timeToSimulate > 0f) {
			foreach (ProjectileModifier modifier in _modifiers)
				modifier.Simulate(Time.deltaTime);
			_timeToSimulate = Time.deltaTime;
		}
	}

	void Update() {
		// Update Size
		_transform.localScale = _originalScale * Size;

		// Update Rigidbody
		_rigidbody.useGravity = Gravity;
		_rigidbody.drag = Drag;

		// Ignore collisions
		if (LifeTime > timeToIgnoreCollisions) {
			IgnoreColliders(ignoreColliders, false);
			ignoreColliders.Clear();
		}
	}

	public void IgnoreCollider(Collider collider, bool ignore = true) {
		foreach (Collider bulletCollider in _bulletColliders)
			Physics.IgnoreCollision(collider, bulletCollider, ignore);
		if (ignore)
			ignoreColliders.Add(collider);
	}

	public void IgnoreColliders(IEnumerable<Collider> colliders, bool ignore = true) {
		foreach (Collider collider in colliders)
			IgnoreCollider(collider, ignore);
	}

	public override void Simulate(float timeToSimulate) {
		_timeToSimulate = timeToSimulate;
	}

	void OnApplicationQuit() {
		_isQuitting = true;
	}

	void OnDestroy() {
		if (_isQuitting)
			return;

		if (NextSection != null)
			NextSection.TransitionModule.StartTransition(this);
	}
}
