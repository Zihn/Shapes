using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (ShapeCreator))]
public class ShapeCreatorEditor : Editor {

	public override void OnInspectorGUI() {
		ShapeCreator SC = (ShapeCreator)target;
		DrawDefaultInspector();

		if (GUILayout.Button ("Spawn Shapes")) {
			SC.SpawnRandomTestObjects ();
		}

		if (GUILayout.Button ("Random Scale")) {
			SC.ScaleShapes ();
		}

		if (GUILayout.Button ("Destroy All")) {
			SC.DestroyAllShapes ();
		}
	}
}
