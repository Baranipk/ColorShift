using Cysharp.Threading.Tasks;
using UnityEngine;

public class KeyInteraction : MonoBehaviour ,IInteractable
{
	[SerializeField] private Door door;
	public void Interact()
	{
		
	}

	public void OnStepOn(PlayerController player)
	{
		OnCollected();
		SoundManager.Instance.Get("DoorOpen").Play();
		GameObject.Destroy(gameObject);
	}

	private  void OnCollected()
	{
		door.open();
	}
}
