using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public enum PlayerColors
{
    white,
    green,
    pink,
    yellow,
    blue
}
public class PlayerColorHandler : MonoBehaviour
{
    [HideInInspector] public PlayerColors currentColor = PlayerColors.white;

    
    private SpriteRenderer spriteRenderer;
	private int _baseColorId;
    private bool isColorChaging = false;
	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		_baseColorId = Shader.PropertyToID("_BaseColor");
	}

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Q) && !isColorChaging)
        {
           ChanceRandomColor();
        }
	}
	public async UniTaskVoid ChanceRandomColor()
    {
		PlayerColors randomColor;
        do
        {
			int i = Random.Range(0, 4);
			switch (i)
			{
				case 0:
					randomColor = PlayerColors.green;
					break;
				case 1:
					randomColor = PlayerColors.yellow;
					break;
				case 2:
					randomColor = PlayerColors.blue;
					break;
				case 3:
					randomColor = PlayerColors.pink;
					break;
				default:
					randomColor = PlayerColors.white;
					break;
			}
		}
        while (randomColor == currentColor);
		 await ChangeColor(randomColor);
	}

    public async UniTask ChangeColor(PlayerColors playerColor, float duration= 1f)
    {
        isColorChaging = true;
        Color color = Color.white;

		currentColor = playerColor;

        color = ColorSO.GetColorFromPlayerColors(playerColor);
		EventBus<OnColorChanged>.Publish(new OnColorChanged() { PlayerColor = currentColor });
        await spriteRenderer.material.DOColor(color, _baseColorId, duration)
            .SetEase(Ease.InOutQuad)
            .ToUniTask();
        isColorChaging = false;
		 
	}
}
