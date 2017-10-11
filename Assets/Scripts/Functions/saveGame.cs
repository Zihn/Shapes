using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class saveGame : MonoBehaviour, IPointerClickHandler {

	public GameObject ui;


	public void OnPointerClick(PointerEventData eventData) {
		ObjectSelector.RemoveSelectedShape();		
		StartCoroutine(CaptureScreen ());
		GameState.SaveGame (GameState.currentGameName);

	}

	public static IEnumerator CaptureScreen()
	{
		string imName = GameState.currentGameName + "_p.png";
		CanvasGroup ui = GameObject.FindGameObjectWithTag ("UI").GetComponent<CanvasGroup> ();

		yield return null;
		ui.alpha = 0;

		yield return new WaitForEndOfFrame();

		Application.CaptureScreenshot(imName);
		ui.alpha = 1;
	}
}
