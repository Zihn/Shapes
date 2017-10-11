using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class popMainMenu : MonoBehaviour, IPointerClickHandler {

	private GameObject menu;
	public loadCreations creations;
	private CanvasGroup ui;

	void Start() {
		menu = GameObject.FindGameObjectWithTag ("MainMenu");
		ui = GameObject.FindGameObjectWithTag ("UI").GetComponent<CanvasGroup> ();
	}

	public void OnPointerClick(PointerEventData eventData) {
		ObjectSelector.RemoveSelectedShape();		
		StartCoroutine (CaptureScreen ());
		GameState.SaveGame (GameState.currentGameName);
	}

	public IEnumerator CaptureScreen()
	{
		string imName = "/" + GameState.currentGameName + ".png";

//		ui.alpha = 0;
//		yield return null;

//		yield return new WaitForEndOfFrame();

		if (GameState.gameChangedSinceLoad) {
			ui.alpha = 0;
			Application.CaptureScreenshot (Application.persistentDataPath + imName);
			yield return null;
			ui.alpha = 1;
			creations.LoadCreations ();
		}
		menu.SetActive (true);
	}
}