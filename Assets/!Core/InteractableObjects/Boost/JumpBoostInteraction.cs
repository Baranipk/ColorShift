using UnityEngine;

public class JumpBoostInteraction : MonoBehaviour, IInteractable
{
	public void Interact()
	{}

	public void OnStepOn(PlayerController player)
	{
		player.GetComponent<PlayerMovement>().isDoubleJump = true;
		SoundManager.Instance.Get("BoosterCollect").Play();
		Destroy(gameObject);
	}
}
