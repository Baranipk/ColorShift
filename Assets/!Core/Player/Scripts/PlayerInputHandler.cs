using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerAction _playerInput;
    private PlayerController _playerController;
    
    void Awake()
    {
        _playerInput = new PlayerAction();
        _playerController = GetComponent<PlayerController>();
        
    }

    private void OnEnable()
    {
        _playerInput.Player.Enable();
        _playerInput.Player.Jump.performed += JumpPressed;
        _playerInput.Player.Pause.performed += PausePresed;

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
            _playerController.playerStateMachine.ChangeState(_playerController.jumpState); 
        }
    }

    public void PausePresed(InputAction.CallbackContext context){
		if (context.ReadValueAsButton())
		{
            EventBus<OnPausePressed>.Publish(new OnPausePressed());
		}
	}


	public void ActivateInput()
    {
        _playerInput.Player.Enable();
    }

    public void DeactivateInput() 
    {
        _playerInput.Player.Disable();
    }

    
}
