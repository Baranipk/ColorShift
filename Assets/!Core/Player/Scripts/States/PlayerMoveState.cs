using UnityEngine;

public class PlayerMoveState : IplayerState
{
    PlayerMovement _playerMovement;
    Rigidbody2D rigidbody;
    PlayerController Controller;

    public PlayerMoveState(PlayerController controller)
    {
        Controller = controller;
        _playerMovement = controller.gameObject.GetComponent<PlayerMovement>(); 
        rigidbody = controller.gameObject.GetComponent<Rigidbody2D>();
        
    }
    public void Enter(){
        Debug.Log("Movement Statete girild");
    }
    public void Exit(){
        Debug.Log("Movement Stateten çýkýldý");
    }
    public void FixedUpdate()
    {
        _playerMovement.Move();
    }
    public void Update(){
        if(rigidbody.linearVelocity.x == 0)
        {
            //rigidbody.linearVelocity = Vector3.zero;
            Controller.playerStateMachine.ChangeState(Controller.idleState);
        }
    }
}
