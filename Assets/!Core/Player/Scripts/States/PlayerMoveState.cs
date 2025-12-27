using UnityEngine;

public class PlayerMoveState : IplayerState
{
    PlayerMovement _playerMovement;
    Rigidbody2D rigidbody;
    PlayerController Controller;
    PlayerAnimation playerAnimation;
    public PlayerMoveState(PlayerController controller)
    {
        Controller = controller;
        _playerMovement = controller.gameObject.GetComponent<PlayerMovement>(); 
        rigidbody = controller.gameObject.GetComponent<Rigidbody2D>();
        playerAnimation = controller.gameObject.GetComponent<PlayerAnimation>();
        
    }
    public void Enter(){
        Debug.Log("Movement Statete girild");
        playerAnimation.SetAnimationWalk();

    }
    public void Exit(){
        Debug.Log("Movement Stateten çýkýldý");
    }
    public void FixedUpdate()
    {
        _playerMovement.Move();
    }
    public void Update(){
        if(Mathf.Abs(rigidbody.linearVelocity.x) <= 0.01f)
        {
            //rigidbody.linearVelocity = Vector3.zero;
            Controller.playerStateMachine.ChangeState(Controller.idleState);
        }
    }
}
