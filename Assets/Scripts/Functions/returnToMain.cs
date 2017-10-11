using UnityEngine;
using UnityEngine.EventSystems;

public class returnToMain : MonoBehaviour, IPointerClickHandler {


	private GameObject mainUI;
	private GameObject colorPicker;

	void Start () {
//		mainUI = GameObject.FindGameObjectWithTag ("MainUI");
		colorPicker = GameObject.FindGameObjectWithTag ("ColorPicker");
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (ColorShortcut.SelectedShortcut) {
			ColorShortcut.RemoveShortcutReference ();
//			ColorPicker.RemoveTarget (ColorShortcut.SelectedShortcut);
		}
		colorPicker.SetActive (false);
//		mainUI.SetActive (true);
	}
}
