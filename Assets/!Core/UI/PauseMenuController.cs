using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
	[Header("UI Referanslarý")]
	[SerializeField] private GameObject menuContainer; // Açýp kapatacaðýmýz ana obje
	[SerializeField] private RectTransform contentRect; // Hareket edecek olan kutu (Panel)
	[SerializeField] private GameObject BlackPanel;

	[Header("Animasyon Ayarlarý")]
	[SerializeField] private float animDuration = 0.5f;
	[SerializeField] private Ease openEase = Ease.OutBack; // Açýlýrken yaylanma
	[SerializeField] private Ease closeEase = Ease.InBack; // Kapanýrken içeri kaçma

	// Menü kapalýyken Y ekseninde nerede duracak? (Ekranýn üstünde, görünmez bir yer)
	[SerializeField] private float startPosY = 1000f;

	private bool _isBusy = false;
	private bool _isMenuOpen = false;
	private EventBinding<OnPausePressed> _onPausePressed;
	private void OnEnable()
	{
		_onPausePressed = new EventBinding<OnPausePressed>(HandlePauseInput);
		EventBus<OnPausePressed>.Subscribe(_onPausePressed);
	}

	private void OnDisable()
	{
		EventBus<OnPausePressed>.UnSubscribe(_onPausePressed);
	}
	private void Start()
	{
		// Baþlangýçta menüyü gizle
		//menuContainer.SetActive(false);
		BlackPanel.SetActive(false);
		// Paneli yukarýya (baþlangýç noktasýna) ýþýnla
		contentRect.anchoredPosition = new Vector2(0, startPosY);
	}
	public void HandlePauseInput()
	{
		if (_isBusy) return;

		if (_isMenuOpen)
		{
			// Menü zaten açýksa, kapat
			ClosePauseMenu().Forget();
		}
		else
		{
			// Menü kapalýysa, aç
			OpenPauseMenu().Forget();
		}
	}
	public async UniTaskVoid OpenPauseMenu()
	{
		if (_isBusy) return;
		_isBusy = true;

		// 1. Menüyü görünür yap
		//menuContainer.SetActive(true);
		BlackPanel.SetActive(true);
		// 2. Zamaný durdur
		Time.timeScale = 0f;

		// 3. Paneli yukarýdan (startPosY) merkeze (0) indir
		// SetUpdate(true) UNUTMA: Oyun donukken animasyonun çalýþmasý için þart.
		await contentRect.DOAnchorPosY(0, animDuration)
						 .SetEase(openEase)
						 .SetUpdate(true)
						 .ToUniTask();

		_isBusy = false;
		_isMenuOpen =true;
	}

	public async UniTask ClosePauseMenu()
	{
		if (_isBusy) return;
		_isBusy = true;

		// 1. Paneli merkezden (0) yukarýya (startPosY) gönder
		await contentRect.DOAnchorPosY(startPosY, animDuration)
						 .SetEase(closeEase)
						 .SetUpdate(true)
						 .ToUniTask();

		// 2. Menüyü gizle (Animasyon bittikten sonra)
		//menuContainer.SetActive(false);
		BlackPanel.SetActive(false);
		// 3. Zamaný tekrar akýt
		Time.timeScale = 1f;

		_isBusy = false;
		_isMenuOpen = false;
	}

}
