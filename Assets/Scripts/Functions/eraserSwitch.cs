using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class eraserSwitch : MonoBehaviour, IPointerClickHandler {

	private bool switched = false;

	public void OnPointerClick(PointerEventData eventData) {
		Circle C = gameObject.GetComponent<Circle> ();
		switched = !switched;
		C.fill = !switched;
		spawnShapes.Switch ();
		//		C.OnRebuildRequested (); //Does not build
	}
}
