using UnityEngine;

public class PlatformInteraction : MonoBehaviour
{
	[SerializeField] ColorSO colors;

	[SerializeField] private PlayerColors currentColor = PlayerColors.white;

	private SpriteRenderer spriteRenderer;
	private Collider2D OBJcollider;

	private EventBinding<OnColorChanged> colorChangedEventBinding;
	private void Awake()
	{
		OBJcollider = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	private void OnEnable()
	{
		colorChangedEventBinding = new EventBinding<OnColorChanged>(Initilize);
		EventBus<OnColorChanged>.Subscribe(colorChangedEventBinding);
	}
	private void OnDisable()
	{
		EventBus<OnColorChanged>.UnSubscribe(colorChangedEventBinding);
	}
	private void Start()
	{
		spriteRenderer.color = ColorSO.GetColorFromPlayerColors(currentColor, colors);

		if(currentColor == PlayerColors.white)
		{
			OBJcollider.enabled = true;
		}
		else
		{
			OBJcollider.enabled = false;
		}
	}

	public void Initilize(OnColorChanged onColorChanged)
	{
		if (currentColor == PlayerColors.white) return;
		if (currentColor == onColorChanged.PlayerColor)
		{
			OBJcollider.enabled = true;
		}
		else
		{
			OBJcollider.enabled = false;
		}
	}
}
