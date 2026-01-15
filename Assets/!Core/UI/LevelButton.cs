using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private Button btn;

	private int sceneIndexToLoad;

	// Menü oluþturucu bu fonksiyonu çaðýracak
	public void Setup(string displayName, int sceneIndex)
	{
		levelText.text = displayName;
		sceneIndexToLoad = sceneIndex;

		btn.onClick.RemoveAllListeners();
		btn.onClick.AddListener(async () =>
		{
			// Týklanýnca LevelManager'daki yeni fonksiyonu çaðýrýyoruz
			//SceneTransitionManager.Instance.gameObject.SetActive(true);
			await SceneTransitionManager.Instance.CloseCurtainAsync();
			LevelManager.Instance.LoadSpecificLevel(sceneIndexToLoad).Forget();
		});
	}
}
