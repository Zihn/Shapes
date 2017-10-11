using UnityEngine;
using UnityEngine.EventSystems;

public class colorQuickBar : MonoBehaviour,IPointerClickHandler {

	// Clicking this color will change the shape to this color.
	// but clicking this color and then clicking the colorpicker will change the shortcut
	// the colorpickerbutton background will change when this color is picked as well..

	public void OnPointerClick(PointerEventData eventData)
	{
		//		Debug.Log ("color " + gameObject.GetComponent<Shape>().color);
		if (ObjectSelector.SelectedShape) {
			ObjectSelector.SelectedShape.color = gameObject.GetComponent<Shape> ().color;
		}
	}
}
