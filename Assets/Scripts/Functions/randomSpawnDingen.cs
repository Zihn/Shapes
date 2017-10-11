using UnityEngine;
using UnityEngine.EventSystems;

public class randomSpawnDingen : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler {
	ColorizeObjects CO;
	ShapeCreator SC;
	SnapToGrid grid;
	ObjectSelector OS;
	Material material;
	float width;
	float height;
	Vector3 startPos;
	Vector3 pos;
	public GameObject Background;

	void Start () {
		GameObject Controller = GameObject.FindGameObjectWithTag ("Magic");
		CO = Controller.GetComponent<ColorizeObjects> ();
		SC = Controller.GetComponent<ShapeCreator> ();
		grid = Controller.GetComponent<SnapToGrid> ();
		OS = Controller.GetComponent<ObjectSelector> ();
		material = Background.GetComponent<Shape> ().material;
		width = Background.GetComponent<RectTransform> ().rect.width;
		height = Background.GetComponent<RectTransform> ().rect.height;
		Debug.Log (Background.GetComponent<RectTransform> ().rect.width);
		material.SetFloat ("_Width", width);
		material.SetFloat ("_Height", height);
//		material.SetFloat ("_XPosition", t);
//		material.SetFloat ("_YPosition", t);

	}

	void Update() {
		float t = Time.timeSinceLevelLoad;
		material.SetFloat ("_T", t);
	}
	
	// Update is called once per frame
	public void OnPointerClick (PointerEventData eventData) {
//		SC.spaw
	}

	public void OnBeginDrag (PointerEventData eventData) {
		startPos = Input.mousePosition;
	}

	public void OnDrag (PointerEventData eventData) {
		Vector3 diff = Input.mousePosition - startPos;
		if (diff.x == 0) {
			material.SetFloat ("_Width", 1);
		} else {
			material.SetFloat ("_Width", Mathf.Abs(diff.x));
		}

		if (diff.y == 0) {
			material.SetFloat ("_Height", 1);
		} else {
			material.SetFloat ("_Height", Mathf.Abs(diff.y));
		}
	}

	public void OnEndDrag (PointerEventData eventData) {

	}
}
