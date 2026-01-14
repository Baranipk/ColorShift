using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BumperInteraction : MonoBehaviour, IInteractable
{
	[SerializeField] private float punchStrength = 0.1f; // Ne kadar büyüyecek (0.1 birim)
	[SerializeField] private float duration = 0.2f;

	[SerializeField] float thrust = 10f;

	[Header("Colliders")]
	[SerializeField] private BumperCollider left;
	[SerializeField] private BumperCollider right;
	[SerializeField] private BumperCollider up;
	[SerializeField] private BumperCollider Down;
	public void Interact()
	{

	}

	public void OnStepOn(PlayerController player)
	{
		BumperAnim().Forget();
		player.playerStateMachine.ChangeState(player.bumperState);
		Rigidbody2D playerRb = player.gameObject.GetComponent<Rigidbody2D>();
		//playerRb.linearVelocity = Vector2.zero;
		Vector2 direction = Vector2.zero;
		if (left.Dir == 1)
		{
			direction.x = -1;
		}
		// Sað collider tetiklendiyse, oyuncuyu SAÐA (1) fýrlat
		else if (right.Dir == 1)
		{
			direction.x = 1;
		}

		// --- DÝKEY KONTROL ---
		// Üst collider tetiklendiyse, YUKARI (1) fýrlat
		if (up.Dir == 1)
		{
			direction.y = 1;
		}
		// Alt collider tetiklendiyse, AÞAÐI (-1) fýrlat
		else if (Down.Dir == 1)
		{
			direction.y = -1;
		}


		Debug.Log($"dir {direction}");
		playerRb.AddForce(direction * thrust,ForceMode2D.Impulse);
	}

	public async UniTask BumperAnim()
	{
		var token = this.GetCancellationTokenOnDestroy();
		await transform.DOPunchScale(Vector3.one * punchStrength, duration, 10, 1)
					   .ToUniTask(cancellationToken: token);
	}
}
