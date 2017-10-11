using UnityEngine;
using UnityEngine.EventSystems;

public class ColorSBPicker : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler  
{
	public GameObject sbBackground;
//	public Shape pickPreview;
	public int size = 40;
	public float increasedSize = 1.5f;
	public float increasedBGSize = 1.5f;
	public bool increaseBackground = true;
	private Vector2 BGSize;
	private static Material sbMaterial;
	private static float width;
	private static float height;
	private RectTransform rectTrans;
	private RectTransform bgTrans;
	private GameObject colorPicker;
	private static Vector2 pickerPosition;
	private GameObject mainUI;
	private Vector2 shift;

	void Awake() {
		ColorPicker.csbp = gameObject.GetComponent<ColorSBPicker> ();
		bgTrans = sbBackground.GetComponent<RectTransform> ();
		sbMaterial = sbBackground.GetComponent<Shape> ().material;
		rectTrans = gameObject.GetComponent<RectTransform> ();
		width = bgTrans.rect.width * (increaseBackground ? increasedBGSize : 1);
		height = bgTrans.rect.height * (increaseBackground ? increasedBGSize : 1);
		shift = new Vector2( bgTrans.position.x, bgTrans.position.y);
		BGSize = bgTrans.sizeDelta;
//		if (pickPreview) {
//			ColorPicker.SetPickPreview (pickPreview);
//		}
	}
		
	public void OnPointerDown (PointerEventData eventData)
	{
		// TODO: Animate it
		rectTrans.sizeDelta = new Vector2(increasedSize*size, increasedSize*size);
		if (increaseBackground) {
			bgTrans.sizeDelta = new Vector2 (increasedBGSize*width/2, increasedBGSize*height/2);
		}
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		// TODO: ANimate
//		float newSize = Mathf.Lerp (increasedSize, size, Time.time);
		rectTrans.sizeDelta = new Vector2(size, size);
		if (increaseBackground) {
			bgTrans.sizeDelta = BGSize;
		}
	}
		
	public void OnDrag (PointerEventData eventData)
	{
		pickerPosition = SetPickerPositionFromScreen (Input.mousePosition);
		ColorPicker.SetTargetColor ();
	}

	Vector2 SetPickerPositionFromScreen(Vector3 pos)
	{ 
		float newPosX = Mathf.Clamp (pos.x - shift.x, -width / 2, width/2);
		float newPosY = Mathf.Clamp (pos.y - shift.y, -height / 2, height/2);
		rectTrans.localPosition = new Vector3 (newPosX, newPosY, 0f);
		return new Vector2 (newPosX, newPosY);
	}

	public void SetPickerPositionFromSB( float s, float b){
		float x = s * width - (width / 2);
		float y = b * height - (height / 2);
		rectTrans.localPosition = new Vector3 (x, y, 0);
		pickerPosition = rectTrans.localPosition;
		gameObject.transform.GetChild (0).GetComponent<Shape> ().color = ColorPicker.GetColor ();
	}

	public static void SetHue(float hue)
	{
		sbMaterial.color = new HSBColor(hue, 1, 1).ToColor();
	}

	public static Vector2 GetSBValues()
	{
		float s = (pickerPosition.x + width / 2)/width;
		float b = (pickerPosition.y + height / 2)/height;
		return new Vector2(s, b);
	}

	bool inRect(Vector3 pos) 
	{
		return false;
	}
}