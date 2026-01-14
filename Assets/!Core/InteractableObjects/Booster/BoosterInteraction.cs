using UnityEngine;

public enum BoosterType
{
	Up,
	UperLeft,
	UperRight,
}
public class BoosterInteraction : MonoBehaviour, IInteractable
{
	[SerializeField] private PlayerColors currentColor;
	[SerializeField] private BoosterType boosterType = BoosterType.Up;
	[SerializeField] private float boostForce = 5f;
	[SerializeField] private Sprite UpSprite;
	[SerializeField] private Sprite UperLeftSprite;
	[SerializeField] private Sprite UperRightSprite;

	private SpriteRenderer spriteRenderer;
	
	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		Initilize();
	}
	public void Interact()
	{}

	public void OnStepOn(PlayerController player)
	{
		if (currentColor == PlayerColors.white || player.GetComponent<PlayerColorHandler>().currentColor == currentColor)
		{
			player.GetComponent<PlayerBoost>().Boost(boosterType,boostForce);
		}
	}

	public void Initilize()
	{
		switch (boosterType)
		{
			case BoosterType.Up:
				spriteRenderer.sprite = UpSprite;
				break;
			case BoosterType.UperLeft:
				spriteRenderer.sprite = UperLeftSprite;
				break;
			case BoosterType.UperRight:
				spriteRenderer.sprite = UperRightSprite;
				break;
			default:
				spriteRenderer.sprite = UpSprite;
				break;
		}

		spriteRenderer.color = ColorSO.GetColorFromPlayerColors(currentColor);
	}

}
