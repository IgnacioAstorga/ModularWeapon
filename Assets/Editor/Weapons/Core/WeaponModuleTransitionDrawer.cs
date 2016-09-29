using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(WeaponModuleTransition))]
public class WeaponModuleTransitionDrawer : PropertyDrawer {

	public static readonly float VERTICAL_MARGIN = 0f;
	public static readonly int NUMBER_OF_PROPERTIES = 1;

	private bool showFold;
	private float height;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		EditorGUI.BeginProperty(position, label, property);
		Rect rect = new Rect(position.x, position.y, position.width, height);

		showFold = EditorGUI.Foldout(GetNextRect(ref rect, height), showFold, "Transition Module");
		if (showFold)
			EditorGUI.PropertyField(GetNextRect(ref rect, height), property.FindPropertyRelative("fireRate"), new GUIContent("Fire Rate"));

		EditorGUI.EndProperty();
	}

	public Rect GetNextRect(ref Rect rect, float height) {
		Rect positionRect = new Rect(rect);
		rect.y += height + VERTICAL_MARGIN;
		return positionRect;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		height = base.GetPropertyHeight(property, label);
		if (showFold)
			return height + NUMBER_OF_PROPERTIES * (height + VERTICAL_MARGIN);
		else
			return height;
	}
}
