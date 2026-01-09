using Cysharp.Threading.Tasks;
using UnityEngine;

public class KeyInteraction : MonoBehaviour ,IInteractable
{
	
	public void Interact()
	{
		
	}

	public async void OnStepOn(PlayerController player)
	{
		await OnCollected();
	}

	private async UniTask OnCollected()
	{
		
	}
}
