using UnityEngine;

public class SpikeInteraction : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        
    }

    public void OnStepOn(PlayerController player)
    {
        player.playerStateMachine.ChangeState(player.deathState);
    }
}
