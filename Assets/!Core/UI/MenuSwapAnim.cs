using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MenuSwapAnim : MonoBehaviour
{
	[Header("Paneller (RectTransform)")]
	public RectTransform mainMenuRect;
	public RectTransform levelSelectRect;

	[Header("Ayarlar")]
	public float animationDuration = 0.6f;
	public Ease animationEase = Ease.InOutBack; // Hafif yaylanarak gelir, çok þýk durur

	// Butonlara hýzlýca art arda basýlmasýný engellemek için kilit
	private bool isAnimating = false;
	private float screenHeight;

	void Start()
	{
		// Ekran yüksekliðini al (Böylece 1080p, 4K veya mobilde de tam ekran dýþýna çýkar)
		screenHeight = Screen.height;

		// Baþlangýç konumlarýný garantiye alalým
		mainMenuRect.anchoredPosition = Vector2.zero;
		levelSelectRect.anchoredPosition = new Vector2(0, -screenHeight);
	}

	// PLAY Butonuna baðlanacak fonksiyon
	public async void ShowLevelSelection()
	{
		if (isAnimating) return; // Animasyon bitmeden tekrar basýlamasýn
		isAnimating = true;

		// --- ASENKRON ANÝMASYON BAÞLIYOR ---

		// 1. Görev: Ana menü YUKARI gitsin (Ekran dýþýna)
		var taskMain = mainMenuRect.DOAnchorPosY(screenHeight, animationDuration)
			.SetEase(animationEase)
			.ToUniTask(); // DOTween'i UniTask'e çeviriyoruz

		// 2. Görev: Level menüsü ORTAYA gelsin (Aþaðýdan yukarý)
		var taskLevel = levelSelectRect.DOAnchorPosY(0, animationDuration)
			.SetEase(animationEase)
			.ToUniTask();

		// Ýki animasyonu ayný anda baþlat ve ikisi de bitene kadar bekle
		await UniTask.WhenAll(taskMain, taskLevel);

		isAnimating = false;
		Debug.Log("Level Seçme Menüsü Açýldý!");
	}

	// BACK (Geri) Butonuna baðlanacak fonksiyon
	public async void BackToMainMenu()
	{
		if (isAnimating) return;
		isAnimating = true;

		// --- GERÝ DÖNÜÞ ANÝMASYONU ---

		// 1. Level menüsü tekrar AÞAÐI insin
		var taskLevel = levelSelectRect.DOAnchorPosY(-screenHeight, animationDuration)
			.SetEase(animationEase) // Ýstersen Ease.InBack yapabilirsin çýkýþ için
			.ToUniTask();

		// 2. Ana menü tekrar ORTAYA insin
		var taskMain = mainMenuRect.DOAnchorPosY(0, animationDuration)
			.SetEase(animationEase)
			.ToUniTask();

		await UniTask.WhenAll(taskMain, taskLevel);

		isAnimating = false;
		Debug.Log("Ana Menüye Dönüldü!");
	}
}
