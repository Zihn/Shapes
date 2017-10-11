using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class dragModifier: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
	[Range(0.01f, 1f)]
	public float size = 0.3f;
	private ObjectSelector OS;
	private Vector3 modification;
	private Shape selectedShape;
	private Shape selectedOutline;
	private float baseWidth;
	private float baseHeight;
	private float sign;
	private Vector2 posCorrection;
	private SnapToGrid grid;
	private Vector3 startPosition;

	void Start() {
		GameObject Controller = GameObject.FindGameObjectWithTag ("Magic");
		OS = Controller.GetComponent<ObjectSelector> ();
		grid = Controller.GetComponent<SnapToGrid> ();

		gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(size * UIScaler.baseUnit, size * UIScaler.baseUnit);

		GameObject parent = gameObject.transform.parent.gameObject;
		selectedShape = parent.GetComponent<Shape> ();
		selectedOutline = parent.transform.GetChild (0).GetComponent<Shape> ();

		RectTransform parentRect = parent.GetComponent<RectTransform> ();
		baseWidth = parentRect.sizeDelta.x / 2;
		baseHeight = parentRect.sizeDelta.y / 2;

		Rect canvasRect = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<RectTransform> ().rect;
		Vector2 shiftPos = new Vector3 (-canvasRect.width / 2, -canvasRect.height / 2, 0);

		sign = Mathf.Sign (gameObject.transform.localPosition.y);
		if (selectedShape.GetType () == typeof(Ellipse)) {
			float rad = Mathf.Deg2Rad * -45; // make this adjustable
			float c = Mathf.Cos (rad);
			float s = Mathf.Sin (rad);
			posCorrection = new Vector2 (c, s);
		} else {
			posCorrection = new Vector2 (1f, -1f); //make this adjustable from object selector script 
		}
	}
		
	public void OnBeginDrag (PointerEventData eventData)
	{			
		GameState.gameChangedSinceLoad = true;
		startPosition = Input.mousePosition;
		ObjectSelector.DestroyModifyButtons ("ModderCircular");
	}

	public void OnDrag (PointerEventData eventData)
	{	
		modification = Input.mousePosition - startPosition; // just need difference no need for correction
		Vector2 newV = grid.getGridPosition(new Vector2(baseWidth + modification.x, baseHeight + (sign*modification.y)));
		if (newV != Vector2.zero) {
			modifyShape (newV);
		}
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		OS.Redraw ();
	}

	void modifyShape(Vector2 v) {
		selectedShape.SetBoundingBox (v);
		selectedOutline.SetBoundingBox (v);
		gameObject.transform.localPosition = new Vector2(v.x * posCorrection.x, v.y * posCorrection.y);
	}
}