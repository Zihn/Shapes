using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class dragModifierCircular: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[Range(0.01f, 1f)]
	public float size = 0.3f;
	public bool useSnap = true;
	[Range(0.0f, 360f)]
	public float snapDegree = 15;
	private ObjectSelector OS;
	private Vector2 shift;
	private float relativeRotation;
	private float modification;
	private SnapToGrid grid;
	private float startRotation;

	void Start() {
		GameObject Controller = GameObject.FindGameObjectWithTag ("Magic");
		OS = Controller.GetComponent<ObjectSelector> ();
		grid = Controller.GetComponent<SnapToGrid> ();

		gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(size * UIScaler.baseUnit, size * UIScaler.baseUnit);

		Vector2 center = ObjectSelector.SelectedShape.transform.position;
		shift = center;

		Vector2 pos = gameObject.transform.position;
		relativeRotation = GetAngleFromXY (pos, center);
	}

	public void OnBeginDrag (PointerEventData eventData)
	{	
		GameState.gameChangedSinceLoad = true;
		startRotation = gameObject.transform.eulerAngles.z*Mathf.Deg2Rad;
		ObjectSelector.DestroyModifyButtons ("Modder");
	}

	public void OnDrag (PointerEventData eventData)
	{	
		modification = ClampRad(GetAngleFromXY(Input.mousePosition, shift) + startRotation - relativeRotation); // just need difference no need for correction
		modifyShape (modification);
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		OS.Redraw ();
	}

	float GetAngleFromXY(Vector2 pos, Vector2 center){
		float rad = ClampRad(Mathf.Atan2 (pos.y - center.y, pos.x - center.x));
		return rad;
	}

	float ClampRad(float rad) {
		rad = rad > 0 ? rad : (2 * Mathf.PI + rad);
		rad = rad < (2*Mathf.PI) ? rad : rad - 2 * Mathf.PI;
		return rad;
	}

	void modifyShape(float rad) {
		float rot = rad * Mathf.Rad2Deg;
		float rotation = useSnap ? Mathf.Round(rot / snapDegree) * snapDegree : rot;
		// ROunding error with quaternion.euler
		ObjectSelector.SelectedShape.transform.localRotation = Quaternion.Euler (0, 0, rotation);
	}
}