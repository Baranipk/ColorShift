using UnityEngine;
using UnityEngine.Rendering;

public class ColoredBlockİnteraction : MonoBehaviour
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
		spriteRenderer.color = ColorSO.GetColorFromPlayerColors(currentColor,colors);
		
	}

	public void Initilize(OnColorChanged onColorChanged) {
		if(currentColor == onColorChanged.PlayerColor)
		{
			OBJcollider.enabled = false;
		}
		else
		{
			OBJcollider.enabled = true;
		}
	}
}
