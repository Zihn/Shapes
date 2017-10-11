using UnityEngine;
using UnityEngine.EventSystems;

public class ColorHPicker : MonoBehaviour, IDragHandler, IBeginDragHandler
{
	public GameObject HBackground;
	public bool circularScale = false;
	public bool turnableScale = false;
	private float width;
	private float height;
	private RectTransform rectTrans;
	private RectTransform bgTrans;
	private float startRad;
	private float wheelRad;
	private static float hueValue;
	private Vector2 shift;

	void Start() {
		ColorPicker.chp = gameObject.GetComponent<ColorHPicker> ();
		bgTrans = HBackground.GetComponent<RectTransform> ();
		rectTrans = gameObject.GetComponent<RectTransform> ();
		width = bgTrans.rect.width;
		height = bgTrans.rect.height;
		Vector2 p = rectTrans.anchoredPosition;
		shift = new Vector2( bgTrans.position.x, bgTrans.position.y);
		GameObject cp = GameObject.FindGameObjectWithTag ("ColorWheel");
		setShaderOrigin (cp.GetComponent<RectTransform>().anchoredPosition);

		float hue = circularScale ? width*(turnableScale ? 0.125f : -0.25f) : p.x;
		if (turnableScale) {
			Material m = HBackground.GetComponent<Shape> ().material;
			m.SetFloat ("_Rotation", 0);
		}
		ColorSBPicker.SetHue (GetHueValueFromPoint (hue));
		ColorPicker.SetTargetColor ();
	}

	public void OnBeginDrag (PointerEventData eventData){
		startRad = ClampRad(GetAngleFromXY(Input.mousePosition) + wheelRad);
	}

	public void OnDrag (PointerEventData eventData)
	{
		float hueValue = SetPickerPositionFromScreen (Input.mousePosition);
		ColorSBPicker.SetHue (hueValue);
		ColorPicker.SetTargetColor ();
	}

	float SetPickerPositionFromScreen(Vector3 pos)
	{
		float scalePosition;

		//TODO: optimize this 
		if (circularScale) {
			float rad = GetAngleFromXY (pos);
			if (turnableScale) {
				rad = ClampRad(rad - startRad);
				Material m = HBackground.GetComponent<Shape> ().material;
				m.SetFloat ("_Rotation", -rad);
				wheelRad = -rad;

				rad = ClampRad(-rad + 1.25f*Mathf.PI);
			} else {
				GameObject parent = gameObject.transform.parent.gameObject;
				parent.transform.localRotation = Quaternion.Euler (0, 0, Mathf.Rad2Deg * rad - 90);
			}
			scalePosition = ((rad / (2 * Mathf.PI)) * width) - width / 2;
		}else {
			float newPosX = Mathf.Clamp (pos.x - shift.x, -width / 2, width/2);
			float newPosY = rectTrans.localPosition.y;
			scalePosition = newPosX;
			rectTrans.localPosition = new Vector3 (newPosX, newPosY, 0f);
		}
		return GetHueValueFromPoint(scalePosition);
	}

	public void SetPickerPositionFromHue(float hue){
		hueValue = hue;
		if (circularScale) {
			float rad = ClampRad(hue*2*Mathf.PI - 1.25f*Mathf.PI);
			if (turnableScale) {
				Material m = HBackground.GetComponent<Shape> ().material;
				m.SetFloat ("_Rotation", rad);
				wheelRad = rad;
			} else {
				GameObject parent = gameObject.transform.parent.gameObject;
				parent.transform.localRotation = Quaternion.Euler (0, 0, Mathf.Rad2Deg * rad - 90);
			}
		} else {
			SetPickerPositionFromScreen(new Vector3(width * hue, 0, 0));
		}
	}

	float GetHueValueFromPoint(float pos)
	{
		float max = width;
		pos += max / 2;
		hueValue = pos / max;
		return hueValue;
	}

	float GetAngleFromXY(Vector2 pos){
		float rad = ClampRad(Mathf.Atan2 (pos.y - shift.y, pos.x - shift.x));
		return rad;
	}

	float ClampRad(float rad) {
		rad = rad > 0 ? rad : (2 * Mathf.PI + rad);
		rad = rad < (2*Mathf.PI) ? rad : rad - 2 * Mathf.PI;
		return rad;
	}

	public static float GetCurrentHue()
	{
		return hueValue;
	}

	bool inRect(Vector3 pos) 
	{
		return false;
	}

	// dynamic radial shader
	void setShaderOrigin(Vector2 s){
		Material m = HBackground.GetComponent<Shape> ().material;
		m.SetFloat ("_XPosition", s.x);
		m.SetFloat ("_YPosition", s.y);
	}
}