using UnityEngine;
using UnityEngine.EventSystems;

public class loadGame : MonoBehaviour, IPointerClickHandler {

	public void OnPointerClick(PointerEventData eventData) {
		GameObject.FindGameObjectWithTag ("MainMenu").SetActive(false);
		ObjectSelector.RemoveSelectedShape();
		if (gameObject.name == "NewGame") {
			GameState.LoadNewGame ();
//			GameState.newGame = true;
		} else {
			GameState.LoadGame (gameObject.name);
//			GameState.newGame = false;
		}
	}
}
