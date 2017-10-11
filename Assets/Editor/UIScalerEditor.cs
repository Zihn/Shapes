using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (UIScaler))]
public class UIScalerEditor : Editor {

//	SerializedProperty spawnTopPercentage;
//	SerializedProperty spawnLeftPercentage;
//
//	void OnEnable()
//	{
//		spawnTopPercentage = serializedObject.FindProperty("spawnTopPercentage");
//		spawnLeftPercentage = serializedObject.FindProperty("spawnLeftPercentage");
//	}

	public override void OnInspectorGUI() {
		UIScaler US = (UIScaler)target;
		DrawDefaultInspector();
//		EditorGUILayout.LabelField("Spawn Buttons Positon");
//		spawnTopPercentage.floatValue = EditorGUILayout.Slider ("\tTop %", 0.0f, 0, 100);
//		spawnLeftPercentage.floatValue = EditorGUILayout.Slider ("\tLeft %", 0.0f, 0, 100);

		if (GUILayout.Button ("Scale")) {
			US.ScaleUI ();
		}

//		serializedObject.Update();
	}
}
