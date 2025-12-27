using UnityEngine;

public interface IInteractable 
{
    void Interact();
    void OnStepOn(PlayerController player); 
}
