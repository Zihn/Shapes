using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class loadCreations : MonoBehaviour {

	public RectTransform scrollbar;
	private int counter;
//	private RectTransform newGameTile; 
	void Start() {
		LoadCreations ();
//		newGameTile = gameObject.transform.FindChild ("NewGame").GetComponent<RectTransform> ();
	}

	public void AddNewCreationToOverview(){
		counter++;
		positionCreationsFrame (counter);
		createPreviewTile (counter, GameState.currentGameName, loadPreviewImage (GameState.currentGameName));
	}

	public void LoadCreations() {
//		Debug.Log ("Loading creations....");

		int i = 1;
		if (!Directory.Exists (Application.persistentDataPath + "/saves/")) {
			Directory.CreateDirectory (Application.persistentDataPath + "/saves/");
		}
		DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/saves/");
		FileInfo[] files = dir.GetFiles();
		createPreviewTile (i, "NewGame", null);

//		Load backwards
		for (int n = files.Length-1; n >= 0; n--)
		{
			FileInfo file = files [n];
			if (file.Extension.Contains ("bin")) {
				i++;
				string name = file.Name.Split ('.') [0];
				createPreviewTile (i, name, loadPreviewImage(name));
			}
		}
//		foreach (FileInfo file in files)
//		{
//			if (file.Extension.Contains ("bin")) {
//				i++;
//				string name = file.Name.Split ('.') [0];
//				createPreviewTile (i, name, loadPreviewImage(name));
//			}
//		}
		counter = i;
		positionCreationsFrame (i);
	}

	private void createPreviewTile(int index, string name, Texture2D image){
		// Create Tile and load the texture
//		float v = Mathf.Floor(index/2)*UIScaler.baseUnit*2;
		float xPos = UIScaler.baseUnit*1.5f * Mathf.Sign(0 - (index%2));
		float yPos = UIScaler.baseUnit*1.5f + Mathf.Floor(index/3)*UIScaler.baseUnit*3;
		GameObject tmp = Instantiate(Resources.Load("CreationTile"), new Vector3(xPos, -yPos, 0), Quaternion.identity) as GameObject;
		tmp.name = name;
		tmp.transform.SetParent (transform, false);
		tmp.GetComponent<RawImage> ().texture = image;

		RectTransform r = tmp.GetComponent<RectTransform> ();
		Vector2 anchor = new Vector2 (0.5f, 1);
		r.sizeDelta = new Vector2 (UIScaler.baseUnit * 2, UIScaler.baseUnit * 2);
	}

	private Texture2D loadPreviewImage(string name){
		Texture2D tex = null;
		byte[] fileData;
		string filePath = GameState.SaveLocation( name + ".png");

		if (File.Exists(filePath))     {
//			Debug.Log (filePath);
			fileData = File.ReadAllBytes(filePath);
			Texture2D tmp = new Texture2D(2, 2);
			tmp.LoadImage(fileData);
			tex = makeSquareImage (tmp, name);

		}
		return tex;
	}

	private Texture2D makeSquareImage( Texture2D image, string name) {
		if (image.width == image.height) {
			return image;
		}
		Texture2D tex = new Texture2D (image.height, image.height);

		int borderwidth = Mathf.FloorToInt((image.height - image.width)/2);
		for (int i = 0; i < tex.width; i++) {
			for (int j = 0; j < tex.height; j++) {
				if (i > borderwidth && i < borderwidth+image.width) {
					tex.SetPixel (i, j, image.GetPixel(i-borderwidth, j));
				}else{
					tex.SetPixel (i, j, Color.white);
				}
			}
		}
		tex.Apply ();

		byte[] bytes = tex.EncodeToPNG();
		File.WriteAllBytes(GameState.SaveLocation( name + ".png"), bytes);
		return tex;
	}

	private void positionCreationsFrame(int n) {
		float height = UIScaler.baseUnit*1.5f + Mathf.Ceil(1.0f*n/2) * 2 * UIScaler.baseUnit;
		gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -height*0.5f - UIScaler.baseUnit);
		gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (UIScaler.screenShort, height);
		adjustScrollBar (height);
	}

	private void adjustScrollBar(float h) {
		if (scrollbar) {
			float refH = UIScaler.screenLong - 2 * UIScaler.baseUnit;
			float scale = refH / h;
			Color c = scrollbar.GetComponent<Rectangle> ().color;
			if (scale > 1) {
				scrollbar.GetComponent<Rectangle> ().color = new Color(c.r, c.g, c.b, 0);
			} else {
				scrollbar.GetComponent<Rectangle> ().color = new Color(c.r, c.g, c.b, 1);
				float size = refH * scale;
				scrollbar.sizeDelta = new Vector2 (scrollbar.sizeDelta.x, size);
			}
		}
	}
}
