using UnityEngine;
using UnityEngine.EventSystems;
//using System.Collections;

public class ColorShortcut : MonoBehaviour, IPointerClickHandler {

	public static GameObject SelectedShortcut;
	public static GameObject SelectedShortcutLink;
	public GameObject colorQuickBarLink;

	// Clicking this color will change the shape to this color.
	// but clicking this color and then clicking the colorpicker will change the shortcut
	// the colorpickerbutton background will change when this color is picked as well..

	public void OnPointerClick(PointerEventData eventData)
	{
//		Debug.Log ("color " + gameObject.GetComponent<Shape>().color);
		setHighlight ();
	}

	void setHighlight() {
		RemoveShortcutReference ();
		ColorPicker.SetColor (gameObject.GetComponent<Shape> ().color);
		if (colorQuickBarLink) {
			ColorPicker.SetTarget (colorQuickBarLink);
		}
		ColorShortcut.SelectedShortcut = gameObject;
		ColorShortcut.SelectedShortcutLink = colorQuickBarLink;
		ColorPicker.SetTarget (SelectedShortcut);
		GameObject prefab = Resources.Load ("Highlight") as GameObject;
		GameObject tmp = Instantiate(prefab, prefab.transform.localPosition, prefab.transform.rotation) as GameObject;
		tmp.transform.SetParent(gameObject.transform, false);
	}

	public static void RemoveShortcutReference() {
		Destroy (GameObject.FindGameObjectWithTag ("SelectionHighlight"));
		if (ColorShortcut.SelectedShortcut) {
			ColorPicker.RemoveTarget (ColorShortcut.SelectedShortcut);
		}
		if (ColorShortcut.SelectedShortcutLink) {
			ColorPicker.RemoveTarget (ColorShortcut.SelectedShortcutLink);
		}
	}
}
