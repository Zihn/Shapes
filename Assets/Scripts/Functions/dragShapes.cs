using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class dragShapes: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {

	public static GameObject itemBeingDragged;
	GameObject startParent;
	RectTransform rectTrans;
	private Vector3 relativePosition;
	private Vector3 shiftPos;
	private Vector3 newPos;
	GameObject[] otherShapeObjects;
	ColorizeObjects CO;
	ShapeCreator SC;
	SnapToGrid grid;
	ObjectSelector OS;

	void Start() {
		GameObject Controller = GameObject.FindGameObjectWithTag ("Magic");
		CO = Controller.GetComponent<ColorizeObjects> ();
		SC = Controller.GetComponent<ShapeCreator> ();
		grid = Controller.GetComponent<SnapToGrid> ();
		OS = Controller.GetComponent<ObjectSelector> ();
		rectTrans = gameObject.GetComponent<RectTransform> ();

		// No need for a position shift since .position is used with screen- overlay
		startParent = GameObject.FindGameObjectWithTag ("Canvas");
		Rect parentRect = startParent.GetComponent<RectTransform> ().rect;
		shiftPos = new Vector3 (-parentRect.width / 2, -parentRect.height / 2, 0);
	}

	public void OnPointerClick(PointerEventData eventData) {
		OS.SetSelectedShape (gameObject.GetComponent<Shape> ());
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		GameState.gameChangedSinceLoad = true;
		itemBeingDragged = gameObject;
		relativePosition = gameObject.transform.position - Input.mousePosition;
//		otherShapeObjects = GameObject.FindGameObjectsWithTag ("shape");
	}

	public void OnDrag (PointerEventData eventData)
	{	
		newPos = Input.mousePosition + relativePosition;
		newPos = grid.getGridPosition (newPos);
		rectTrans.position = newPos;
//		checkNeighbors ();
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		//Trashbin 
		//TODO: Values needs to change with UI scaling
		bool xCheck = (Input.mousePosition.x > (-shiftPos.x * 2) - 50);
		bool yCheck = (Input.mousePosition.y > (-shiftPos.y * 2) - 50);
		if (xCheck && yCheck) {
			Destroy (gameObject);
		}
		itemBeingDragged = null;
		otherShapeObjects = null;
	}

	void checkNeighbors()
	{	
		SC.DestroyDebug ();
		foreach (GameObject obj in otherShapeObjects) 
		{
			if (obj != gameObject) {
				float dist = Vector3.Distance (rectTrans.localPosition, obj.GetComponent<RectTransform> ().localPosition);
				if (dist < 2 * rectTrans.rect.width) {
					float t = (2 * rectTrans.rect.width) / (dist+0.01f) * 5;
//					Vector3 pos = obj.GetComponent<RectTransform> ().localPosition;
					Shape shape = obj.GetComponent<Shape> ();
					Vector3[] overLapVs = gameObject.GetComponent<Shape> ().OverlapVertices (shape);

					foreach (Vector3 v in overLapVs) {
						SC.DrawDebugCircle (v, t);
					}

//					Vector2[] veees = overLapVs;
//					DrawPolygon (overLapVs);
//					drawLine (newPos, obj.GetComponent<RectTransform>().localPosition, t);
//					drawCircle (newPos, t*2);
//					drawCircle (pos, t*2);

//					Vector3[] vees = obj.GetComponent<Shape> ().getVertices();
//					foreach (Vector3 v in vees) {
////						drawCircle (v+pos, t);
//						counter++;
//						bool inside = gameObject.GetComponent<Shape> ().IsInShape(v+pos);
//						if (inside) {
//							drawCircle (v+pos, t);
//						}
////						Debug.Log (gameObject.GetComponent<Shape> ().IsInShape(v+pos));
//					}


//					CO.ColorizeShape (gameObject.name);
//					CO.ColorizeShape (obj.name);
				}
			}
		}
	}

//	void DrawPolygon(Vector3[] vs)
//	{
//		GameObject pol = GameObject.FindGameObjectWithTag ("polygon");
//		Polygon P = pol.GetComponent<Polygon> ();
//		P.DrawPolygon (vs);
//	}
}