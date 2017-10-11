using UnityEngine;

public class ColorPicker : MonoBehaviour 
{
	private static Color selectedColor;
	public static ColorHPicker chp;
	public static ColorSBPicker csbp;

	public static Color GetColor()
	{
		Vector2 sb = ColorSBPicker.GetSBValues ();
		float h = ColorHPicker.GetCurrentHue ();

		return new HSBColor (h, sb.x, sb.y).ToColor ();
	}

	public static void SetColor(Color c){
		HSBColor hsb = HSBColor.FromColor (c);
		ColorPicker.selectedColor = c;
		chp.SetPickerPositionFromHue (hsb.h);
		ColorSBPicker.SetHue (hsb.h);
		csbp.SetPickerPositionFromSB (hsb.s, hsb.b);
	}

	public static void SetTargetColor()
	{
		selectedColor = GetColor();
		GameObject [] targets = GameObject.FindGameObjectsWithTag("ColorTarget");

		foreach (GameObject target in targets) {
			Shape shape = target.GetComponent<Shape> ();
			shape.color = selectedColor;
		}
	}

	public static void SetTarget(GameObject obj)
	{
		obj.tag = "ColorTarget";
	}

	public static void RemoveTarget(GameObject obj, string tag = "Untagged")
	{
		obj.tag = tag;
	}
}