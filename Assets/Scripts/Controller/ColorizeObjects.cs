using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ColorizeObjects : MonoBehaviour {
	public Color[] baseColors;
	public bool allowNoFill = false;
	public bool randomThickness = false;
	public float tMin = 0.5f;
	public float tMax = 3f;

//	public void ColorizeUnlitColor()
//	{
//		GameObject[] objects = GameObject.FindGameObjectsWithTag ("shape");
//		foreach (GameObject obj in objects) 
//		{
//			int c = Random.Range (0, baseColors.Length);
//			Color colour = baseColors [c];
//			Material newMaterial = new Material(Shader.Find("Unlit/Color"));
//			newMaterial.SetColor ("_Color", colour);
//			obj.GetComponent<MeshRenderer> ().material = newMaterial;
//		}
//	}
	public void ColorizeShape(string name) 
	{
		GameObject obj = GameObject.Find (name);
		Colorize (obj);
	}

	public void ColorizeAll() 
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("shape");

		foreach (GameObject obj in objects) {
			Colorize (obj);
		}

	}

	void Colorize(GameObject obj)
	{
		int c = Random.Range (0, baseColors.Length);
		Color colour = baseColors [c];
		float thickness = tMin;
		if (randomThickness) {
			thickness = Random.Range (tMin, tMax);
		}
		bool fill = fillOrNoFill ();

		obj.GetComponent<Shape> ().color = colour;
		obj.GetComponent<Shape> ().fill = fill;
		obj.GetComponent<Shape> ().thickness = thickness;
	}

	bool fillOrNoFill (){
		if (allowNoFill) {
			int f = Random.Range (0, 2);
			if (f == 0) {
				return true; 
			} else {
				return false;
			}
		}
		return true;
	}
}