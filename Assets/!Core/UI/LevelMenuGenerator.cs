using UnityEngine;

public class LevelMenuGenerator : MonoBehaviour
{
	[Header("UI Elemanlarý")]
	[SerializeField] private GameObject levelButtonPrefab; // Hazýrladýðýn buton prefabý
	[SerializeField] private Transform gridContainer;      // Grid Layout Group olan obje

	private void Start()
	{
		GenerateUI();
		Time.timeScale = 1.0f;
	}

	private void GenerateUI()
	{
		// 1. Önce temizlik (Test amaçlý koyduklarýný siler)
		foreach (Transform child in gridContainer)
		{
			Destroy(child.gameObject);
		}

		// 2. LevelManager'daki listeye ulaþýp butonlarý oluþturuyoruz
		// Singleton olduðu için Instance üzerinden eriþebiliriz.
		if (LevelManager.Instance != null && LevelManager.Instance.levels != null)
		{
			foreach (var levelData in LevelManager.Instance.levels)
			{
				GameObject newBtn = Instantiate(levelButtonPrefab, gridContainer);
				LevelButton btnScript = newBtn.GetComponent<LevelButton>();

				if (btnScript != null)
				{
					// Butona ismini ve hangi sahneyi açacaðýný gönderiyoruz
					btnScript.Setup(levelData.levelDisplayName, levelData.sceneBuildIndex);
				}
			}
		}
		else
		{
			Debug.LogWarning("LevelManager bulunamadý veya Level Listesi boþ!");
		}
	}
}
