using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class spawnShapes : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	private int shapeNR;
	private GameObject Controller;
	private ColorizeObjects CO;
	private ShapeCreator SC;
	private Vector3 newPos;
	private Vector3 shiftPos;
	private static bool spawnErasers = false;
	GameObject startParent;
	GameObject itemBeingDragged;
	Vector3 startPosition;
	RectTransform rectTrans;
	GameObject[] objects;
	SnapToGrid grid;

	void Start()
	{
		Controller = GameObject.FindGameObjectWithTag ("Magic");
		CO = Controller.GetComponent<ColorizeObjects> ();
		SC = Controller.GetComponent<ShapeCreator> ();
		grid = Controller.GetComponent<SnapToGrid> ();

		rectTrans = gameObject.GetComponent<RectTransform> ();
		startPosition = rectTrans.localPosition;

		// No need for a position shift since .position is used with screen- overlay
//		startParent = GameObject.FindGameObjectWithTag ("Canvas");
//		Rect parentRect = startParent.GetComponent<RectTransform> ().rect;
//
//		shiftPos = new Vector3 (-parentRect.width / 2, -parentRect.height / 2, 0);

		if (gameObject.GetComponent<Shape>().GetType() == typeof(Circle) ) {
			shapeNR = 0;
		}else if (gameObject.GetComponent<Shape>().GetType() == typeof(Triangle)) {
			shapeNR = 1;
		}else{
			shapeNR = 2;
		}	
	}

	void spawnShape(float x, float y) 
	{
		if (spawnErasers) {
			int ID = SC.CreateEraser (shapeNR, x, y, UIScaler.baseUnit);
			string name = "shape" + ID;
		} else {
			int ID = SC.CreateShape (shapeNR, x, y, UIScaler.baseUnit);
			string name = "shape" + ID;
			CO.ColorizeShape (name);
		}
	}

	public static void Switch() 
	{
		spawnErasers = !spawnErasers;
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		GameState.gameChangedSinceLoad = true;
		objects = GameObject.FindGameObjectsWithTag ("shape");
	}

	public void OnDrag (PointerEventData eventData)
	{			
		newPos = Input.mousePosition;
		newPos = grid.getGridPosition (newPos);
		rectTrans.position = newPos;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
//		newPos = Input.mousePosition + shiftPos;
		spawnShape (newPos.x, newPos.y);
		rectTrans.localPosition = startPosition;
		objects = null;
	}
}