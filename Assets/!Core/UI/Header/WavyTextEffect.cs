using DG.Tweening;
using TMPro;
using UnityEngine;

public class WavyTextEffect : MonoBehaviour
{
	public TMP_Text textComponent;

	[Header("Dalga Ayarlarý")]
	public float waveSpeed = 4.0f;     // Dalganýn akýþ hýzý
	public float waveFrequency = 2.0f; // Dalgalarýn sýklýðý (Ne kadar sýký)
	public float waveHeight = 10.0f;   // Dalganýn yüksekliði (Amplitude)

	[Header("DOTween Kontrolü")]
	[Range(0, 1)]
	public float effectIntensity = 0f; // 0 = Düz yazý, 1 = Tam dalga

	void Start()
	{
		if (textComponent == null) textComponent = GetComponent<TMP_Text>();

		// DOTween ile efekti yumuþakça baþlat (0'dan 1'e 2 saniyede geç)
		DOTween.To(() => effectIntensity, x => effectIntensity = x, 1f, 2f)
			.SetEase(Ease.OutQuad);
	}

	void Update()
	{
		// Yazýnýn mesh bilgisini güncellemeye zorla
		textComponent.ForceMeshUpdate();

		var textInfo = textComponent.textInfo;
		int characterCount = textInfo.characterCount;

		for (int i = 0; i < characterCount; i++)
		{
			var charInfo = textInfo.characterInfo[i];

			// Görünmeyen karakterleri (boþluk gibi) atla
			if (!charInfo.isVisible) continue;

			// Karakterin köþe (vertex) indekslerini al
			var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

			// --- SÝNÜS MATEMATÝÐÝ ---
			// Her harf için bir ofset (kayma) deðeri hesapla
			// i harfin sýrasý, Time.time zaman
			float sinValue = Mathf.Sin(Time.time * waveSpeed + i * waveFrequency);

			// Bu deðeri yükseklik ve DOTween'den gelen effectIntensity ile çarp
			float yOffset = sinValue * waveHeight * effectIntensity;

			// Harfin 4 köþesini de (Kare þeklindedir) yukarý/aþaðý oynat
			// charInfo.vertexIndex -> O harfin ilk köþesinin dizideki yeridir
			for (int j = 0; j < 4; j++)
			{
				var orig = verts[charInfo.vertexIndex + j];
				verts[charInfo.vertexIndex + j] = orig + new Vector3(0, yOffset, 0);
			}
		}

		// Deðiþiklikleri Mesh'e iþle
		for (int i = 0; i < textInfo.meshInfo.Length; i++)
		{
			var meshInfo = textInfo.meshInfo[i];
			meshInfo.mesh.vertices = meshInfo.vertices;
			textComponent.UpdateGeometry(meshInfo.mesh, i);
		}
	}
}
