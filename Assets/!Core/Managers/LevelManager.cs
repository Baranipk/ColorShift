using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	[Header("Settings")]
	[SerializeField] private float deathDelay = 1.5f; // Ölünce kaç sn beklesin?

	// Çoklu týklamalarý veya ölümleri engellemek için flag
	private bool _isProcessRunning = false;

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

	// Oyuncu öldüðünde dýþarýdan çaðrýlacak metot
	// async UniTaskVoid: Unity eventleri (Button click, Collision) tarafýndan çaðrýlacaksa Void kullanýlýr.
	public async UniTaskVoid HandlePlayerDeath()
	{
		// Eðer zaten bir ölüm veya geçiþ iþlemi varsa tekrar çalýþma
		if (_isProcessRunning) return;

		_isProcessRunning = true;
		Debug.Log("UniTask: Oyuncu öldü, iþlemler baþlatýlýyor...");

		// 1. Bekleme Süresi (Coroutine'deki yield return new WaitForSeconds yerine)
		// cancellationToken: Eðer bu bekleme sýrasýnda obje yok olursa hata vermemesi için.
		await UniTask.Delay((int)(deathDelay * 1000), cancellationToken: this.GetCancellationTokenOnDestroy());

		// 2. Sahneyi Asenkron Yükle
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		await SceneManager.LoadSceneAsync(currentSceneIndex).ToUniTask();

		// Ýþlem bitti, bayraðý indir
		_isProcessRunning = false;
	}

	// Seviye geçiþi için de benzer bir yapý kullanabilirsin
	public async UniTaskVoid LoadNextLevel()
	{
		if (_isProcessRunning) return;
		_isProcessRunning = true;

		await UniTask.Delay(1000, cancellationToken: this.GetCancellationTokenOnDestroy());

		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

		// Sahne var mý kontrolü (Basitçe)
		if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
		{
			await SceneManager.LoadSceneAsync(nextSceneIndex).ToUniTask();
		}
		else
		{
			Debug.Log("Oyun Bitti!");
			// Ana menüye dön vs.
			await SceneManager.LoadSceneAsync(0).ToUniTask();
		}

		_isProcessRunning = false;
	}
}
