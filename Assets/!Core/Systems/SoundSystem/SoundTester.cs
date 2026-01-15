using UnityEngine;

public class SoundTester : MonoBehaviour
{
	void Start()
	{
		// 1. Oyun baþlar baþlamaz çalmayý dene
		Debug.Log("Test: Ses çalýnmaya çalýþýlýyor...");

		// Hata ayýklamalý çaðýrma yöntemi
		PlayTestSound();
	}

	void Update()
	{
		// 2. Space tuþuna basýnca çal (Ýnteraktif test)
		if (Input.GetKeyDown(KeyCode.Y))
		{
			Debug.Log("Space tuþuna basýldý.");

			// Zincirleme metod testi: Sesi biraz incelterek çal
			var soundObj = SoundManager.Instance.Get("TestSound");

			if (soundObj != null)
			{
				soundObj.SetPitch(1.2f).Play();
			}
		}
	}

	void PlayTestSound()
	{
		// SoundManager var mý kontrolü
		if (SoundManager.Instance == null)
		{
			Debug.LogError("HATA: Sahnede SoundManager objesi yok veya scripti takýlý deðil!");
			return;
		}

		// Sesi çekme denemesi
		Sound s = SoundManager.Instance.Get("TestSound"); // Buraya Inspector'daki ismin aynýsýný yaz

		if (s == null)
		{
			Debug.LogError("HATA: 'TestSound' isminde bir ses bulunamadý! Ýsimleri kontrol et.");
		}
		else
		{
			s.Play();
			Debug.Log("BAÞARILI: Ses bulundu ve çalýnýyor.");
		}
	}
}
