using UnityEngine;
using UnityEngine.EventSystems;

public class popColor : MonoBehaviour, IPointerClickHandler {

	private GameObject colorPicker;
//	private GameObject mainUI;

	void Start() {
		colorPicker = GameObject.FindGameObjectWithTag ("ColorPicker");
//		mainUI = GameObject.FindGameObjectWithTag ("MainUI");
		colorPicker.SetActive (false);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		colorPicker.SetActive (true);
//		mainUI.SetActive (false);
	}
}