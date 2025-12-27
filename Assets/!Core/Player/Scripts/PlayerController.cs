using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStateMachine playerStateMachine { get; set; }

    public PlayerIdleState idleState;
    public PlayerMoveState moveState;
    public PlayerJumpState jumpState;
    public PlayerDeathState deathState;
    private void Awake()
    {       
        playerStateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        jumpState = new PlayerJumpState(this);
        deathState = new PlayerDeathState(this);
    }

    private void Start() => playerStateMachine.Initialize(idleState);

    private void Update() => playerStateMachine.CurrentState.Update();
    private void FixedUpdate()
    {
        playerStateMachine.ExecuteFixedUpdate();
    }

}
