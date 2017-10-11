using UnityEngine;
using System.Collections;

public class ObjectSelector : MonoBehaviour{

	public int selectionThickness = 3;
	public Color outlineColor = Color.red;
	public bool fillSelected = false;
	public Vector2 knobUV = new Vector2(1f, -1f);
	public Vector2 circularKnobUV = new Vector2(-1f, -1f);

	private Vector2 knobPos;
	private Vector2 circularKnobPos;

//	public Transform SelectionCanvas;
	public static Shape SelectedShape;
		
	public static void RemoveSelectedShape() {
		destroyShapeOutline ();
		DestroyModifyButtons ();
		if (ObjectSelector.SelectedShape) {
			ColorPicker.RemoveTarget (ObjectSelector.SelectedShape.gameObject, "shape");
		}
	}

	public void SetSelectedShape(Shape shape){
		RemoveSelectedShape ();
		ObjectSelector.SelectedShape = shape;
		ColorPicker.SetTarget (shape.gameObject);
//		Debug.Log ("shape " + shape + " selected");

		knobPos = KnobPosition (shape, knobUV);
		circularKnobPos = KnobPosition (shape, circularKnobUV);

		drawShapeOutline ();
	}

	public void Redraw() {
		SetSelectedShape (ObjectSelector.SelectedShape);
	}

	void drawShapeOutline(){
		destroyShapeOutline ();
		GameObject tmp = Instantiate(SelectedShape.gameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		tmp.name = "selectionOutline";
		tmp.tag = "SelectionOutline";
		Destroy(tmp.GetComponent<dragShapes> ());
		Shape outline = tmp.GetComponent<Shape> ();
		outline.color = outlineColor;
		outline.fill = fillSelected;
		outline.thickness = selectionThickness;
		outline.raycastTarget = false;
		tmp.transform.SetParent(ObjectSelector.SelectedShape.transform, false);
		drawModifyButtons ();
	}

	Vector2 KnobPosition(Shape shape, Vector2 u){
		RectTransform r = shape.GetComponent<RectTransform> ();
		float hH = r.rect.height / 2;
		float hW = r.rect.width / 2;
		Vector2 position;
		if (shape.GetType() == typeof(Circle) || shape.GetType() == typeof(Ellipse)) {
			float rad = Mathf.Atan2 (u.y, u.x);
			rad = rad > 0 ? rad : (2 * Mathf.PI + rad);
//			rad = rad < (2*Mathf.PI) ? rad : rad - 2 * Mathf.PI;
			float c = Mathf.Cos(rad);
			float s = Mathf.Sin(rad);
			position = new Vector2(hW * c, hH * s);
		}else{
			position = new Vector2 (u.x*hW, u.y*hH); // Make Adjustable
		}
		return position;
	}

	private static void destroyShapeOutline() {
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("SelectionOutline");
		foreach (GameObject obj in objs) {
			DestroyImmediate (obj); // Why does Destroy() not work?
		}
	}

	void drawModifyButtons() {
		DestroyModifyButtons();
		drawButton ("Modder", knobPos);
		drawButton ("ModderCircular", circularKnobPos);
	}

	void drawButton(string name, Vector2 pos){
		GameObject tmp = Instantiate(Resources.Load(name), new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		tmp.transform.SetParent(ObjectSelector.SelectedShape.transform, false);
		tmp.name = name;
		RectTransform r = tmp.GetComponent<RectTransform> ();
		r.localPosition = pos;
	}
		
	public static void DestroyModifyButtons(string name = null) {
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Modifier");
		foreach (GameObject obj in objs) {
			if (name != null) {
				if (obj.name == name) {
					DestroyImmediate (obj);
				}
			} else {
				DestroyImmediate (obj);
			}
		}
	}
}
