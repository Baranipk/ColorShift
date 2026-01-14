using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerController controller;


	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
	}
	public void Boost(BoosterType boosterType,float force = 5f)
    {
        switch (boosterType)
        {
            case BoosterType.Up:
				rb.linearVelocity = Vector2.zero;
                rb.AddForce(new Vector2(0,1) * force ,ForceMode2D.Impulse);
				controller.playerStateMachine.ChangeState(controller.bumperState);
				Debug.Log("Boost Up");
				break;
            case BoosterType.UperLeft:
				rb.linearVelocity = Vector2.zero;
				rb.AddForce(new Vector2(-1, 1) * force, ForceMode2D.Impulse);
				controller.playerStateMachine.ChangeState(controller.bumperState);
				Debug.Log("Boost UpLeft");
				break;
			case BoosterType.UperRight:
				rb.linearVelocity = Vector2.zero;
				rb.AddForce(new Vector2(1, 1) * force, ForceMode2D.Impulse);
				controller.playerStateMachine.ChangeState(controller.bumperState);
				Debug.Log("Boost UpRight");
				break;
		}
    }
}
