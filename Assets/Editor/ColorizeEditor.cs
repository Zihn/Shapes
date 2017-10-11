using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (ColorizeObjects))]
public class ColorizeObjectsEditor : Editor {

	public override void OnInspectorGUI() {
		ColorizeObjects CO = (ColorizeObjects)target;
		DrawDefaultInspector();

		if (GUILayout.Button ("Colorize")) {
			CO.ColorizeAll ();
		}
	}
}
