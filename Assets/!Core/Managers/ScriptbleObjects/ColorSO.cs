using UnityEngine;

[CreateAssetMenu(fileName ="New Color Data",menuName ="Scriptble Objects/ColorData")]
public class ColorSO : ScriptableObject
{
    public Color whiteColor  = Color.white;
	public Color pinkColor = new Color(0.7765f, 0.3176f, 0.5922f);
	public Color blueColor = new Color(0.3098f, 0.5608f, 0.7294f);
    public Color yellowColor = new Color(0.8706f, 0.6196f, 0.2549f);
    public Color greenColor = new Color(0.4588f, 0.6549f, 0.2627f);


	public static Color GetColorFromPlayerColors(PlayerColors pColor ) {
		switch (pColor) {
			case PlayerColors.pink:
				return new Color(0.7765f, 0.3176f, 0.5922f);
			case PlayerColors.blue:
				return new Color(0.3098f, 0.5608f, 0.7294f);
			case PlayerColors.green:
				return new Color(0.4588f, 0.6549f, 0.2627f);
			case PlayerColors.yellow:
				return new Color(0.8706f, 0.6196f, 0.2549f);
			case PlayerColors.white:
				return Color.white;
			default:
				return Color.white;
		}
	}
	public static Color GetColorFromPlayerColors(PlayerColors pColor,ColorSO so)
	{
		switch (pColor)
		{
			case PlayerColors.pink:
				return so.pinkColor;
			case PlayerColors.blue:
				return so.blueColor;
			case PlayerColors.green:
				return so.greenColor;
			case PlayerColors.yellow:
				return so.yellowColor;
			case PlayerColors.white:
				return Color.white;
			default:
				return Color.white;
		}
	}
}
