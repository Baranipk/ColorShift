using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
	[Header("Ayarlar")]
	public float scaleFactor = 1.1f; // Ne kadar büyüsün? (1.1 = %10 büyüme)
	public float duration = 0.2f;    // Animasyon süresi
	public Ease easeType = Ease.OutBack; // Zýplama efekti için 'OutBack' harikadýr

	private Vector3 originalScale;

	void Start()
	{
		// Butonun orijinal boyutunu hafýzaya alalým
		originalScale = transform.localScale;
	}

	// Mouse butonun üzerine geldiðinde çalýþýr
	public void OnPointerEnter(PointerEventData eventData)
	{
		SoundManager.Instance.Get("UIButton").Play();
		// Önceki animasyonlarý durdur (Hýzlý giriþ çýkýþlarda bozulmasýn)
		transform.DOKill();

		// Hedef boyuta büyüt
		transform.DOScale(originalScale * scaleFactor, duration)
			.SetEase(easeType)
			.SetUpdate(true); // Oyun dursa bile (Pause menüsü) çalýþsýn
	}

	// Mouse butonun üzerinden gittiðinde çalýþýr
	public void OnPointerExit(PointerEventData eventData)
	{
		transform.DOKill();

		// Orijinal boyutuna geri döndür
		transform.DOScale(originalScale, duration)
			.SetEase(Ease.OutQuad) // Dönüþte zýplamaya gerek yok, yumuþak olsun
			.SetUpdate(true);
	}

	// Obje yok olursa tween'i öldür
	void OnDestroy()
	{
		transform.DOKill();
	}
}
