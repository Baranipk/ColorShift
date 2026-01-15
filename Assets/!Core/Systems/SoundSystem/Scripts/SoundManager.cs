using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance;
	public Sound[] sounds;

	void Awake()
	{
		if (Instance == null) Instance = this;
		else { Destroy(gameObject); return; }

		DontDestroyOnLoad(gameObject);

		// AudioSource'larý oluþturma kýsmý ayný
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	// ARTIK NESNE DÖNDÜRÜYORUZ
	public Sound Get(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);

		if (s == null)
		{
			Debug.LogWarning("Ses bulunamadý: " + name);
			return null; // Hata durumunda null döner
		}

		return s; // Bulunan Sound nesnesini olduðu gibi ver
	}

	[ContextMenu("Sounds Settings Set Default")]
	public void ApplyCodeDefaults()
	{
		// 1. Koddaki "public float volume = 0.5f" gibi güncel deðerleri taþýyan
		//    geçici, boþ bir referans nesnesi oluþturuyoruz.
		Sound referansSes = new Sound();

		foreach (Sound s in sounds)
		{
			// 2. Listedeki her sesin AYARLARINI bu referanstan alýyoruz.
			//    DÝKKAT: Name ve Clip'i eþitlemiyoruz, onlar özel kalmalý.

			s.volume = referansSes.volume; // Kodda 0.5f yazdýysan buraya 0.5f gelir
			s.pitch = referansSes.pitch;   // Kodda 1.2f yazdýysan buraya 1.2f gelir

			// Eðer loop varsayýlanýný da deðiþtirdiysen:
			// s.loop = referansSes.loop; 
		}

		Debug.Log($"Tüm sesler koddaki varsayýlan deðerlere (Vol: {referansSes.volume}, Pitch: {referansSes.pitch}) güncellendi!");
	}
}
