using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class UIScaler : MonoBehaviour {
	public RectTransform MainMenu;
	public RectTransform MainCanvas;
	public RectTransform SpawnButtons;
	public RectTransform ColorQuickBar;
	public RectTransform ColorShortcutBars;
	public RectTransform Trashbin;
	public RectTransform ColorPicker;

//	public float pixelsPerUnit = 100;
	[Range(0.001f, 100.0f)]
	public float baseUnitPixelPercentage;
	public static float baseUnit;
	public static bool landscape;
	public static float screenShort;
	public static float screenLong;

	[Header("MainMenu")] 
	public float menuBarHeightMax;

	[Header("Spawn Buttons")]
	[Range(0.0f, 100.0f)]
	public float spawnTop;
	[Range(0.0f, 100.0f)]
	public float spawnLeft;
	[Range(1.0f, 100.0f)]
	public float spawnButtonSize;
	public float spawnButtonSizeMax;

	[Header("Quickbar")]
	[Range(1.0f, 100.0f)]
	public float quickbarHeight;
	public float quickbarHeightMax;
	[Range(1.0f, 100.0f)]
	public float pickColorDotSize;
	public float pickColorDotsMaxSpread;

	[Header("Trashbin")]
	[Range(1.0f, 100.0f)]
	public float trashBinSize;
	[Range(1.0f, 100.0f)]
	public float trashBinDotSize;

	[Header("ColorPicker")] 
	[Range(1.0f, 100.0f)]
	public float colorPickerSize;
	[Range(1.0f, 100.0f)]
	public float colorpickerTopFromSize;
	[Range(1.0f, 100.0f)]
	public float colorPickerMaxVSize;
	[Range(0.001f, 10.000f)]
	public float quickBarHighlightSize;


	void Awake() {
		init ();
	}

	void Start() {
//		screenShort = landscape ? MainCanvas.sizeDelta.y : MainCanvas.sizeDelta.x;
//		screenLong = landscape ? MainCanvas.sizeDelta.x : MainCanvas.sizeDelta.y;
//		baseUnit = screenShort * baseUnitPixelPercentage;
		ScaleUI ();
	}

	void init() {
		landscape = MainCanvas.sizeDelta.x > MainCanvas.sizeDelta.y;
		screenShort = landscape ? MainCanvas.sizeDelta.y : MainCanvas.sizeDelta.x;
		screenLong = landscape ? MainCanvas.sizeDelta.x : MainCanvas.sizeDelta.y;
		baseUnit = screenShort * baseUnitPixelPercentage;
	}

	public void ScaleUI(){
		init ();
		setMainMenu ();
		setSpawners ();
		setTrashbin ();
		setColorPicker ();
		setColorShortCuts ();
	}

	void setMainMenu(){
		float h = Mathf.Clamp(baseUnit, 10f, menuBarHeightMax);
		RectTransform bar = MainMenu.transform.Find ("BarMainMenu").GetComponent<RectTransform>();
		RectTransform display = MainMenu.transform.Find ("DisplayMainMenu").GetComponent<RectTransform>();
		RectTransform header = display.transform.Find ("DisplayHeader").GetComponent<RectTransform>();
		RectTransform scrollbar = display.transform.Find ("ScrollBar").GetComponent<RectTransform>();


		bar.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, h);
		bar.anchoredPosition = new Vector3 (0, h/2, 0);
		display.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, MainCanvas.sizeDelta.y-h);
		display.anchoredPosition = new Vector3 (0, (MainCanvas.sizeDelta.y-h)/-2, 0);
		header.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, h);
		header.anchoredPosition = new Vector3 (0, -h/2, 0);
		scrollbar.anchoredPosition = new Vector2 (-h*0.08f, -(MainCanvas.sizeDelta.y-h)/2);
		scrollbar.sizeDelta = new Vector2 (h * 0.16f, MainCanvas.sizeDelta.y - h*2); 
		setSquaresinRect (0.5f, bar);
	}

	void setSpawners() {
		float x = screenShort;
		float y = screenLong;
		float w = x * spawnButtonSize / 100;
		float left = x * spawnLeft/100;
		float top = y * -spawnTop/100;
		float btnSize = Mathf.Clamp (w, 10f, spawnButtonSizeMax);
		SpawnButtons.anchoredPosition = new Vector3 (left, top - w/2, 0f);
		SpawnButtons.sizeDelta = new Vector2 (btnSize, btnSize*3);

		foreach(Transform sb in SpawnButtons.transform) {
			RectTransform sbR = sb.GetComponent<RectTransform> ();
			sbR.sizeDelta = new Vector2(btnSize, btnSize);
		}
	}

	void setColorPicker(){
		float s = screenShort * colorPickerSize / 100;
		s = s < screenLong * colorPickerMaxVSize/100 ? s : screenLong * colorPickerMaxVSize/100;
		float t = s * colorpickerTopFromSize / 100;
		ColorPicker.sizeDelta = new Vector2 (s, s);
		if (landscape) {
			ColorPicker.anchoredPosition = new Vector3 (-s/1.7f, 0, 0);
			ColorShortcutBars.sizeDelta = new Vector2 (s, s/2);
			ColorShortcutBars.anchoredPosition = new Vector3 (s/1.7f, -s/4, 0);
		} else {
			ColorPicker.anchoredPosition = new Vector3 (0, t, 0);
			ColorShortcutBars.sizeDelta = new Vector2 (s, s/2);
			ColorShortcutBars.anchoredPosition = new Vector3 (0, -t*1.3f, 0);
		}
		RectTransform pointer = ColorPicker.GetChild (0).GetChild (1).Find ("HPicker").GetComponent<RectTransform> ();
		if (pointer) { 
			pointer.sizeDelta = new Vector2 (baseUnit, baseUnit);
		}
	}

	void setColorShortCuts() {

		float h = Mathf.Clamp(MainCanvas.sizeDelta.y * quickbarHeight / 100, 10f, quickbarHeightMax);
		ColorQuickBar.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, h);
		setSquaresinRect (0.5f, ColorQuickBar);
		int i = 0;
		foreach (RectTransform row in ColorShortcutBars) {
			if (row.name.StartsWith ("Row")) {
				row.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, h);
//				row.anchoredPosition = new Vector3 (0, -h / 2 - (h * 1.5f * i), 0);
				setSquaresinRect (0.5f, row);
				i++;
			} else if (row.name == "QuickBarHighlight") {
				float size = ColorShortcutBars.rect.width * quickBarHighlightSize;
				row.sizeDelta = new Vector2 (size, h + (size - ColorShortcutBars.rect.width));
			}
		}

		float w = ColorQuickBar.rect.width / ColorQuickBar.childCount;
		RectTransform returnbtn  = ColorPicker.transform.parent.Find ("ReturnToMain").GetComponent<RectTransform> ();
		returnbtn.sizeDelta = new Vector2 (w, h);
		returnbtn.anchoredPosition = new Vector3 (-w/2, h/2, 0);
		setDotsInRect (returnbtn);

		RectTransform colorpickbtn = ColorQuickBar.Find ("PickColor").GetComponent<RectTransform> ();
		setDotsInRect (colorpickbtn);
	}

	void setSquaresinRect(float s, RectTransform ts) {
		int i = 0;
//		int n = ColorQuickBar.childCount;
		float w = ts.rect.width / ts.childCount;
		foreach(Transform t in ts) {
			RectTransform tR = t.GetComponent<RectTransform> ();
			tR.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, w);
			tR.anchoredPosition = new Vector3 ((s*w) + (i * w),0, 0);
//			tR.pivot = new Vector2 (1, 0.5f);
			i++;
		}
	}

	void setDotsInRect(RectTransform dotsParent){
		Rect r = dotsParent.rect;
		float s = r.width < r.height ? r.width : r.height;
		float d = Mathf.Clamp (r.width * 0.25f, 1f, pickColorDotsMaxSpread);
		int j = 0;
		foreach(Transform dot in dotsParent.transform) {
			RectTransform dotR = dot.GetComponent<RectTransform> ();
			dotR.sizeDelta =  new Vector2(s*pickColorDotSize/100, s*pickColorDotSize/100);
			dotR.anchoredPosition = new Vector3 (d*(-j+1),0, 0);
			j++;
		}
	}

	void setTrashbin(){
		float s = screenShort *  trashBinSize / 100;
		Trashbin.sizeDelta = new Vector2 (s, s/2);
		Trashbin.anchoredPosition = new Vector3 (-s/6, -s/6, 0);
		RectTransform dot = Trashbin.GetChild (0).GetComponent<RectTransform>();
		dot.sizeDelta = new Vector2(s*trashBinDotSize/100, s*trashBinDotSize/100);
		dot.anchoredPosition = new Vector3 (0, -s*trashBinDotSize/200, 0);
	}


}
