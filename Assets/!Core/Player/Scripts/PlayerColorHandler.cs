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

        switch (playerColor)
        {
            case PlayerColors.white:
                color = Color.white;
                break;
			case PlayerColors.green:
				color = new Color(0.4588f,0.6549f,0.2627f);
				break;
			case PlayerColors.pink:
				color = new Color(0.7765f,0.3176f,0.5922f);
				break;
			case PlayerColors.yellow:
				color = new Color(0.8706f,0.6196f,0.2549f);
				break;
			case PlayerColors.blue:
				color = new Color(0.3098f,0.5608f,0.7294f);
				break;
            default:
				color = Color.white;
                break;
		}

        await spriteRenderer.material.DOColor(color, _baseColorId, duration)
            .SetEase(Ease.InOutQuad)
            .ToUniTask();

        await UniTask.Delay(500);
        isColorChaging = false;
	}
}
