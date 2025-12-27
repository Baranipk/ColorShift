using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.OnStepOn(controller);
        }
    }
}
