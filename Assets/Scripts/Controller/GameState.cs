using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public static class GameState {

	[System.Serializable]
	public class ShapeData {
		public string shapeType;
		public ShapeVector sizeDelta;
		public string name;
		public float rotation;
		public ShapeVector position;
		public ShapeColor color;

		public ShapeData (Shape shape){
//			Debug.Log ("Storing data of " + shape.GetType());
			shapeType = shape.GetType().ToString();
			sizeDelta = new ShapeVector(shape.GetComponent<RectTransform>().sizeDelta);
			name = shape.name;
			rotation = shape.transform.eulerAngles.z;
			position = new ShapeVector(shape.transform.position);
			color = new ShapeColor(shape.color);
		}

		[System.Serializable]
		public class ShapeVector
		{
			public float x;
			public float y;

			public ShapeVector(Vector2 v){
				x = v.x;
				y = v.y;
			}

			public Vector2 ToVector(){
				return new Vector2 (this.x, this.y);
			}
		}

		[System.Serializable]
		public class ShapeColor
		{
			public float r;
			public float g;
			public float b;
			public float a;

			public ShapeColor(Color c){
				r = c.r;
				g = c.g;
				b = c.b;
				a = c.a;
			}

			public Color ToColor(){
				return new Color (this.r, this.g, this.b, this.a);
			}
		}
	}

	public static List<ShapeData> SaveData;
	public static string currentGameName;
	public static bool gameChangedSinceLoad = false;
//	public static bool newGame = true;
//	public static string saveLocation = Application.persistentDataPath + "/saves/";

	public static string SaveLocation(string filename){
		string folder = filename.EndsWith (".bin") ? "/saves/" : "/"; 
		return Application.persistentDataPath + folder + filename;
	}

	public static void SaveGame(string name) {
		if (GameState.gameChangedSinceLoad) {
			SaveData = new List<ShapeData> ();
//		Debug.Log ("Game is being saved....");
			GameObject[] shapes = GameObject.FindGameObjectsWithTag ("shape");
			foreach (GameObject shape in shapes) {
				ShapeData data = new ShapeData (shape.GetComponent<Shape> ());
				SaveData.Add (data);
			}
//		Debug.Log ("Writing save file....");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (SaveLocation (name + ".bin"));
			bf.Serialize (file, SaveData);
			file.Close ();
		}
//		Debug.Log ("Done!");
	}

	public static void LoadGame(string name) {
//		Debug.Log ("Loading " + name + "....");
		GameState.currentGameName = name;
		GameState.gameChangedSinceLoad = false;
		if (File.Exists (SaveLocation( name +".bin"))) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (SaveLocation( name +".bin"), FileMode.Open);
			GameState.SaveData = (List<ShapeData>)bf.Deserialize (file);
			file.Close ();
//			Debug.Log ("Drawing Shapes....");
			GameState.DrawShapes ();
		} else {
//			Debug.Log ("No such file....");
//			Debug.Log ("Starting Empty Game...");
			GameState.ClearCanvas ();
		}
	}

	public static void LoadNewGame() {
//		Debug.Log ("New Game is being created....");

		int i = 1;
		DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath + "/saves/");
		FileInfo[] files = dir.GetFiles();
		foreach (FileInfo file in files)
		{
			if (file.Extension.Contains("bin"))
				i++;
		}
		GameState.LoadGame ("save" + i.ToString());
	}

	private static void DrawShapes() {
		GameObject parent = GameObject.FindGameObjectWithTag ("ShapeCanvas");
		GameState.ClearCanvas ();
		int i = 0;
		foreach(ShapeData data in SaveData){
			GameObject tmp = GameObject.Instantiate(Resources.Load(data.shapeType), new Vector3(data.position.x, data.position.y, 1), Quaternion.identity) as GameObject;
			tmp.name = data.name;
			tmp.transform.SetParent(parent.transform, true);
			tmp.GetComponent<RectTransform> ().sizeDelta = data.sizeDelta.ToVector();
			tmp.GetComponent<Shape> ().color = data.color.ToColor();
			tmp.transform.eulerAngles = new Vector3(0, 0, data.rotation);
			i++;
		}
		ShapeCreator.shapeID = i;
//		Debug.Log ("Done!");
	}

	private static void ClearCanvas(){
//		Debug.Log ("Clearing Canvas..");
		GameObject[] shapes = GameObject.FindGameObjectsWithTag ("shape");

		foreach (GameObject shape in shapes) {
			GameObject.Destroy (shape);
		}
	}
}