using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	[Header("Settings")] // Ölünce kaç sn beklesin?
	// Çoklu týklamalarý veya ölümleri engellemek için flag
	private bool _isProcessRunning = false;

	[System.Serializable]
	public class LevelInfo
	{
		public string levelDisplayName; // Örn: "Level 1"
		public int sceneBuildIndex;     // Build Settings'deki index numarasý
	}

	[Header("Level Setup")]
	public List<LevelInfo> levels; // Editörden dolduracaðýn liste burasý
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	public async UniTaskVoid LoadSpecificLevel(int sceneIndex)
	{
		if (_isProcessRunning) return;
		_isProcessRunning = true;

		Debug.Log($"Level {sceneIndex} yükleniyor...");

		// Ýsteðe baðlý: Ufak bir bekleme veya loading ekraný açma
		await UniTask.Delay(100);

		if (sceneIndex < SceneManager.sceneCountInBuildSettings)
		{
			await SceneManager.LoadSceneAsync(sceneIndex).ToUniTask();
		}
		else
		{
			Debug.LogError("Hata: Girilen Scene Index Build Settings'de yok!");
		}

		_isProcessRunning = false;
	}

	// Oyuncu öldüðünde dýþarýdan çaðrýlacak metot
	// async UniTaskVoid: Unity eventleri (Button click, Collision) tarafýndan çaðrýlacaksa Void kullanýlýr.
	public async UniTaskVoid HandlePlayerDeath()
	{
		// Eðer zaten bir ölüm veya geçiþ iþlemi varsa tekrar çalýþma
		if (_isProcessRunning) return;

		_isProcessRunning = true;
		Time.timeScale = 0;
		await SceneTransitionManager.Instance.CloseCurtainAsync();
		// 2. Sahneyi Asenkron Yükle
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		await SceneManager.LoadSceneAsync(currentSceneIndex).ToUniTask();
		await SceneTransitionManager.Instance.OpenCurtainAsync();
		Time.timeScale = 1f;
		// Ýþlem bitti, bayraðý indir
		_isProcessRunning = false;
	}

	// Seviye geçiþi için de benzer bir yapý kullanabilirsin
	public async UniTaskVoid LoadNextLevel()
	{
		if (_isProcessRunning) return;
		_isProcessRunning = true;

		await SceneTransitionManager.Instance.CloseCurtainAsync();

		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

		// Sahne var mý kontrolü (Basitçe)
		if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
		{
			await SceneManager.LoadSceneAsync(nextSceneIndex).ToUniTask();
			await SceneTransitionManager.Instance.OpenCurtainAsync();
		}
		else
		{
			Debug.Log("Oyun Bitti!");
			// Ana menüye dön vs.
			await SceneManager.LoadSceneAsync(0).ToUniTask();
		}

		_isProcessRunning = false;
	}

	public async void ReSpawn()
	{
		HandlePlayerDeath().Forget();
	}

	public async void loadMainMenu() {
		if (_isProcessRunning) return;
		await SceneTransitionManager.Instance.CloseCurtainAsync();
		await SceneManager.LoadSceneAsync(0).ToUniTask();
		await SceneTransitionManager.Instance.OpenCurtainAsync();
	}

	public void QuitGame()
	{
		// Eðer Unity Editör'ün içindeysen, play modunu durdur
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;

		// Eðer oyun Build alýnmýþsa (exe, apk vs.) uygulamayý kapat
#else
            Application.Quit();
#endif
	}
}
