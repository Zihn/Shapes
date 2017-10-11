using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ShapeCreator : MonoBehaviour {
    public GameObject canvas;
	public GameObject circle;
	public GameObject triangle;
	public GameObject rectangle;

	public GameObject canvasEraser;
	public GameObject circleEraser;
	public GameObject triangleEraser;
	public GameObject rectangleEraser;

	public enum SelectShape {circle=0, triangle=1, rectangle=2};
	public SelectShape drawShape;

//	public int gridSize = 0;

	public int spawnNumber = 5;
	public bool colorize = false;
	public bool random = false;
	public bool randomScale = false;
	public bool linearScale = true;
	public float xMin = 1f;
	public float xMax = 1.5f;
	public float yMin = 1f;
	public float yMax = 1.5f;

	public static int shapeID = 0;
	private GameObject[] shapeObjects;
	private GameObject[] eraserObjects;

    void Awake()
    {
		shapeObjects = new GameObject[]{circle, triangle, rectangle};
		eraserObjects = new GameObject[]{circleEraser, triangleEraser, rectangleEraser};
    }

	public void SpawnRandomTestObjects() 
	{
		RectTransform canvasRect = canvas.GetComponent<RectTransform> ();
		int width = (int)canvasRect.rect.width;
		int height = (int)canvasRect.rect.height;
		int padding = (int) Mathf.Round(Mathf.Min (width, height) * 0.2f);
		int id = 0;
		for (int i = 0; i < spawnNumber; i++)
		{	
			int s = (int)drawShape;
			if (random) {
				s = Random.Range (0, 3);   // Get random shape
			}
			float x = Random.Range(padding, width - padding) - width/2;
			float y = Random.Range (padding, height - padding) - height/2;
			CreateShape(s, x, y, 100);
		}
		if (colorize) {
			ColorizeObjects CO = gameObject.GetComponent<ColorizeObjects> ();
			CO.ColorizeAll ();
		}
	}

	public int CreateShape(int shapeNumber, float xPos, float yPos, float size)
	{	
		// Shape Nr: 1 Circle, 2 Triangle, 3 Rectangle
		GameObject shape = shapeObjects[shapeNumber];
		GameObject tmp = Instantiate(shape, new Vector3(xPos, yPos, 1), Quaternion.identity) as GameObject;
		tmp.name = "shape" + shapeID;
		tmp.transform.SetParent(canvas.transform, true);
		tmp.GetComponent<RectTransform> ().sizeDelta = new Vector2(size, size);
		if (randomScale) {
			scaleShape (tmp);
		}
		shapeID++;
		return shapeID - 1;
	}

	public int CreateEraser(int shapeNumber, float xPos, float yPos, float size)
	{	
		// Shape Nr: 1 Circle, 2 Triangle, 3 Rectangle
		GameObject eraser = eraserObjects[shapeNumber];
		GameObject tmp = Instantiate(eraser, new Vector3(xPos, yPos, 1), Quaternion.identity) as GameObject;
		tmp.name = "eraser" + shapeID;
		tmp.transform.SetParent(canvasEraser.transform, false);
		tmp.GetComponent<Shape> ().color = showErasers.colour;
		tmp.GetComponent<RectTransform> ().sizeDelta = new Vector2(size, size);
		if (randomScale) {
			scaleShape (tmp);
		}
		shapeID++;
		return shapeID - 1;
	}

	void scaleShape(GameObject shape) {
		float xScale = Random.Range (xMin, xMax);
		float yScale = Random.Range (yMin, yMax);
		if (linearScale) {
			xScale = yScale;
		}
		RectTransform r = shape.GetComponent<RectTransform> ();
		r.localScale = new Vector3 (xScale, yScale, 0);
	}

	public void ScaleShapes() {
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("shape");

		foreach (GameObject obj in objects) {
			scaleShape (obj);
		}
	}

	public void DrawDebugLine(Vector2 p1, Vector2 p2, float thickness)
	{
		GameObject tmp = Instantiate(Resources.Load("Line"), new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		tmp.name = "DebugLine";
		tmp.tag = "Debug";
		tmp.transform.SetParent(canvas.transform, false);
		tmp.GetComponent<Line> ().DrawLine (p1, p2);
		tmp.GetComponent<Line> ().color = Color.red;
		tmp.GetComponent<Line> ().thickness = thickness;
	}

	public void DrawDebugCircle(Vector3 position, float radius)
	{
		GameObject tmp = Instantiate(Resources.Load("Circle"), new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		tmp.name = "DebugCircle";
		tmp.tag = "Debug";
		tmp.transform.SetParent(canvas.transform, false);
		RectTransform r = tmp.GetComponent<RectTransform> ();
		r.sizeDelta = new Vector2(radius, radius);
		r.localPosition = position;
		tmp.GetComponent<Circle> ().color = Color.red;
	}

	public void DestroyDebug()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Debug");

		foreach (GameObject obj in objs) {
			DestroyImmediate (obj);
		}
	}

	public void DestroyAllShapes() {
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("shape");

		foreach (GameObject obj in objects) {
			DestroyImmediate (obj);
		}
	}

	public void DestroyAllErasers() {
		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Eraser");

		foreach (GameObject obj in objects) {
			DestroyImmediate (obj);
		}
	}
}