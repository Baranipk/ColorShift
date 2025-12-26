using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerAction _playerInput;
    private PlayerMovement _playerMovement;
    
    void Awake()
    {
        _playerInput = new PlayerAction();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
        _playerInput.Player.Jump.performed += JumpPressed;
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
        _playerInput.Player.Jump.performed -= JumpPressed;
    }

    public float GetMoveDirection()
    {
        Vector2 moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
        
        return moveDirection.x;
    }

    public void JumpPressed(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            _playerMovement.Jump();
        }
    }
    
}
