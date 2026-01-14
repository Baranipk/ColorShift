using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStateMachine playerStateMachine { get; set; }

    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerDeathState deathState;
    public PlayerBumperState bumperState;
    private void Awake()
    {       
        playerStateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        jumpState = new PlayerJumpState(this);
        deathState = new PlayerDeathState(this);
        bumperState = new PlayerBumperState(this);
    }

    private void Start() 
    {
        gameObject.transform.position = GameObject.FindGameObjectWithTag("StartPosition").transform.position;
        playerStateMachine.Initialize(idleState);
    }

    private void Update() => playerStateMachine.CurrentState.Update();
    private void FixedUpdate()
    {
        playerStateMachine.ExecuteFixedUpdate();
    }

}
