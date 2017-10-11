using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour {

	public bool useGrid = true;
	public float gridCellSize = 1;
	public bool relativeCellSize = true;
//	public int nrOfCells;
//	public bool useCellSize = true;
	public bool showGrid = true;

//	public bool destroyDebug = false;

	private bool validated = false;
	private Rect screenview;
	private Vector3 shiftPos;
	private ShapeCreator SC;

	void Start() {
		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
		screenview = canvas.GetComponent<RectTransform> ().rect;
		shiftPos = new Vector3 (-screenview.width / 2, -screenview.height / 2, 0);
		SC = gameObject.GetComponent<ShapeCreator> ();
		gridCellSize = gridCellSize / 100 * UIScaler.baseUnit;
	}

	void OnValidate() {
		//Hotfix for debug circle deletion.. Within OnValidate DestroyImmediate cannot be called
		validated = false;
	}

	void Update() {
		if (!validated) {
			validated = true;
			toggleShowGrid ();
		}
	}

	public Vector3 getGridPosition(Vector3 position){
		if (useGrid) {
			float xPos = Mathf.Round (position.x / gridCellSize) * gridCellSize;
			float yPos = Mathf.Round (position.y / gridCellSize) * gridCellSize;
			return inBoundsCheck(new Vector3 (xPos, yPos, 0));
		}
		return position;
	}

	public float getGridSize(){
		if(useGrid){
			return gridCellSize;
		}
		return 1;
	}

	Vector3 inBoundsCheck(Vector3 v) {
		//Check for centered anchor point 

//		float hw = screenview.width;
//		float hH = screenview.height;
//
//		if (Mathf.Abs(v.x) > hW) {
//			v.x = Mathf.Sign (v.x) * hW;
//		}
//
//		if (Mathf.Abs(v.y) > hH){
//			v.y = Mathf.Sign (v.y) * hH;
//		}

		float w = screenview.width;
		float h = screenview.height;

		v.x = v.x > w ? w : v.x;
		v.x = v.x < 0 ? 0 : v.x;
		v.y = v.y > h ? h : v.y;
		v.y = v.y < 0 ? 0 : v.y;

		return v;
	}

	void calculateGridCellSize(){
		// calc the grid cells based on the screenview
//		float d = screenview.width < screenview.height ? screenview.width : screenview.height;
//		gridCellSize = d / nrOfCells;
	}
	void toggleShowGrid (){
		if (showGrid) {
			makeGridVisible ();
		} else {
			makeGridInvisible ();
		}
	}

	void makeGridVisible() {
		SC.DestroyDebug ();
		int xCells = (int)Mathf.Floor(screenview.width / gridCellSize);
		int yCells = (int)Mathf.Floor(screenview.height / gridCellSize);

		if (xCells * yCells > 300) {
			Debug.LogError ("To many cells to draw, will result in unstable Unity behaviour with current implementation");
		} else {
			for (int x = 1; x <= xCells; x++) {
				for (int y = 1; y <= yCells; y++) {
					Vector3 pos = new Vector3 (x * gridCellSize, y * gridCellSize, 0);
					SC.DrawDebugCircle (pos + shiftPos, 3);
				}
			}
		}
	}

	void makeGridInvisible(){
		SC.DestroyDebug ();
	}
}
