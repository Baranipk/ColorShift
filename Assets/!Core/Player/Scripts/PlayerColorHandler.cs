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
	[SerializeField] ParticleSystem changeEffect;
	[SerializeField] TrailRenderer trailRenderer;    
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
		SoundManager.Instance.Get("ColorChange").Play();
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

	public async UniTask ChangeColor(PlayerColors playerColor, float duration = 0.5f)
	{
		isColorChaging = true;

		currentColor = playerColor;
		Color targetColor = ColorSO.GetColorFromPlayerColors(playerColor);

		// Event fýrlatma
		EventBus<OnColorChanged>.Publish(new OnColorChanged() { PlayerColor = currentColor });

		

		// 1. Sprite Rengi Deðiþimi (Task oluþturuyoruz ama await etmiyoruz henüz)
		var spriteTask = spriteRenderer.material.DOColor(targetColor, _baseColorId, duration)
			.SetEase(Ease.InOutQuad)
			.ToUniTask();

		// 2. Trail Rengi Deðiþimi
		// TrailRenderer varsa onun da rengini deðiþtir
		UniTask trailStartTask = UniTask.CompletedTask;
		UniTask trailEndTask = UniTask.CompletedTask;

		
		if (trailRenderer != null)
		{
			// Baþlangýç Rengi
			trailStartTask = DOTween.To(() => trailRenderer.startColor, x => trailRenderer.startColor = x, targetColor, duration)
				.SetEase(Ease.InOutQuad)
				.ToUniTask();

			// Bitiþ Rengi (Eðer ucunun þeffaf olmasýný istersen burayý özelleþtirebilirsin)
			// Örn: Color endColor = new Color(targetColor.r, targetColor.g, targetColor.b, 0);
			trailEndTask = DOTween.To(() => trailRenderer.endColor, x => trailRenderer.endColor = x, targetColor, duration)
				.SetEase(Ease.InOutQuad)
				.ToUniTask();
		}

		

		// Tüm animasyonlarýn (Sprite + Trail Start + Trail End) bitmesini bekle
		await UniTask.WhenAll(spriteTask, trailStartTask, trailEndTask);

		if (changeEffect != null)
		{
			// Particle rengini deðiþtirmek için "main" modülünü almalýyýz
			var mainModule = changeEffect.main;
			mainModule.startColor = targetColor; // Rengi ayarla

		    changeEffect.Stop(); // Varsa eskisini durdur
			changeEffect.Play(); // Efekti patlat!
		}
		isColorChaging = false;
	}
}
