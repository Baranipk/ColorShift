using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
	public static SceneTransitionManager Instance { get; private set; }

	[Header("UI Referanslarý")]
	[SerializeField] private RectTransform blackPanelRect;
	// CanvasGroup sildik, gerek yok.

	[Header("Ayarlar")]
	[SerializeField] private float animDuration = 0.5f;
	[SerializeField] private Ease inEase = Ease.OutExpo; // Ýnerken
	[SerializeField] private Ease outEase = Ease.InExpo; // Giderken

	private float _screenHeight;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);

			_screenHeight = blackPanelRect.rect.height;
			if (_screenHeight == 0) _screenHeight = 2000f;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		// Baþlangýçta paneli yukarý ýþýnla ve kapat
		//blackPanelRect.anchoredPosition = new Vector2(0, _screenHeight);
		//blackPanelRect.gameObject.SetActive(false);
		OpenCurtainAsync().Forget();
	}

	// 1. Perdeyi Ýndir
	public async UniTask CloseCurtainAsync()
	{
		// Paneli aktif ettiðimiz an, eðer Image'da "Raycast Target" açýksa týklamalar engellenir.
		blackPanelRect.gameObject.SetActive(true);

		// Tepeye al (Garanti olsun)
		blackPanelRect.anchoredPosition = new Vector2(0, _screenHeight);

		// Yukarýdan -> Merkeze (0) indir
		await blackPanelRect.DOAnchorPosY(0, animDuration)
							.SetEase(inEase)
							.SetUpdate(true)
							.ToUniTask();
	}

	// 2. Perdeyi Kaldýr
	public async UniTask OpenCurtainAsync()
	{
		Time.timeScale = 0f;
		// Merkezden -> Aþaðýya (-height) gönder
		await blackPanelRect.DOAnchorPosY(-_screenHeight, animDuration)
							.SetEase(outEase)
							.SetUpdate(true)
							.ToUniTask();

		// Ýþlem bitti, paneli kapat (Artýk arkasý týklanabilir)
		blackPanelRect.gameObject.SetActive(false);

		// Bir sonraki sefer için gizlice tepeye ýþýnla
		blackPanelRect.anchoredPosition = new Vector2(0, _screenHeight);
		Time.timeScale = 1f;
	}
}
