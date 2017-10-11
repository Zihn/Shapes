using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class removeSelection : MonoBehaviour, IPointerClickHandler {

	public void OnPointerClick(PointerEventData eventData) {
		ObjectSelector.RemoveSelectedShape();
//		Debug.Log ("deselect");
	}
}
