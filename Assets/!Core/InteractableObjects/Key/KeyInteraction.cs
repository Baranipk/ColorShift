using Cysharp.Threading.Tasks;
using UnityEngine;

public class KeyInteraction : MonoBehaviour ,IInteractable
{
	[SerializeField] private Door door;
	public void Interact()
	{
		
	}

	public async void OnStepOn(PlayerController player)
	{
		await OnCollected();
		GameObject.Destroy(gameObject);
	}

	private async UniTask OnCollected()
	{
		door.open();
	}
}
