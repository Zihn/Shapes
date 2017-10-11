using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class showErasers : MonoBehaviour, IPointerClickHandler {

	private bool shown = false;
	public static Color colour = new Color();
	public Color visibleColour = new Color ();
	public Color invisibleColour = new Color ();

	public GameObject showIcon;
	public GameObject hiddenIcon;

	void Awake() {
		showIcon.SetActive(shown);
		colour = invisibleColour;
		changeVisibility ();
		changeButtonApearance ();
	}

	public void OnPointerClick(PointerEventData eventData) {
		changeColour ();
		changeVisibility ();
		changeButtonApearance ();
	}

	void changeButtonApearance()
	{
		showIcon.SetActive(shown);
		hiddenIcon.SetActive(!shown);
	}

	void changeColour() {
		if (shown) {
			shown = false;
			colour = invisibleColour;
		} else {
			shown = true;
			colour = visibleColour;
		}
	}

	void changeVisibility() 
	{	
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Eraser");

		foreach (GameObject obj in objects) {
			obj.GetComponent<Shape> ().color = colour;
		}
	}
}
