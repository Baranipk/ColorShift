using UnityEngine;

public class FinishInteraction : MonoBehaviour, IInteractable
{
	public void Interact()
	{}

	public void OnStepOn(PlayerController player)
	{
		LevelManager.Instance.LoadNextLevel().Forget();
		SoundManager.Instance.Get("FinishLine").Play();
	}
}
