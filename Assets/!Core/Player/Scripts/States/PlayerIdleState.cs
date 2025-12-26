using UnityEngine;

public class PlayerIdleState : IplayerState
{
    Animator animator;
    PlayerController controller;
    PlayerInputHandler inputHandler;

    public PlayerIdleState(PlayerController controller)
    {
        this.controller = controller;

        animator = controller.gameObject.GetComponent<Animator>();
        inputHandler = controller.gameObject.GetComponent<PlayerInputHandler>();

    }
    public void Enter(){
        Debug.Log("Idle Statete girildi");
    }

    public void Exit(){
        Debug.Log("Idle Stateden çýkýldý");
    }

    public void FixedUpdate(){}

    public void Update(){
        if (inputHandler.GetMoveDirection() != 0)
        {
            controller.playerStateMachine.ChangeState(controller.moveState);
        }
    }
}
