using UnityEngine;

public class PlayerMoveState : IplayerState
{
    PlayerMovement _playerMovement;
    Rigidbody2D rigidbody;
    PlayerController Controller;
    PlayerAnimation playerAnimation;

	private float footstepTimer = 0f;
	private float footstepInterval = 0.4f; // Örn: Her 0.3 saniyede bir adým sesi (Animasyon hýzýna göre ayarla)
	public PlayerMoveState(PlayerController controller)
    {
        Controller = controller;
        _playerMovement = controller.gameObject.GetComponent<PlayerMovement>(); 
        rigidbody = controller.gameObject.GetComponent<Rigidbody2D>();
        playerAnimation = controller.gameObject.GetComponent<PlayerAnimation>();
        
    }
    public void Enter(){      
        playerAnimation.SetAnimationWalk();

    }
    public void Exit(){
        
    }
    public void FixedUpdate()
    {
        _playerMovement.Move();
    }
    public void Update(){
        if (_playerMovement.IsGrounded())
        {
            HandleFootsteps();
        }
        
		if (Mathf.Abs(rigidbody.linearVelocity.x) <= 0.01f)
        {
            //rigidbody.linearVelocity = Vector3.zero;
            Controller.playerStateMachine.ChangeState(Controller.idleState);
        }
    }

	private void HandleFootsteps()
	{
		footstepTimer -= Time.deltaTime; // Süreyi azalt

		if (footstepTimer <= 0)
		{
			// SoundManager üzerinden sesi çek
			Sound s = SoundManager.Instance.Get("Walk"); // Buradaki "Footstep" ismini kendi ses isminle deðiþtir

			if (s != null)
			{
				// Rastgelelik ekle (0.8 ile 1.2 arasý pitch)
				float randomPitch = Random.Range(0.8f, 1.2f);
				float randomVolume = Random.Range(0.8f, 1.0f); // Hafif ses þiddeti deðiþimi de doðallýk katar

				// Sesi ayarla ve çal (PlayOneShot üst üste binmeye izin verir)
				s.SetPitch(randomPitch)
				 .SetVolume(randomVolume)
				 .PlayOneShot();
			}

			// Sayacý tekrar kur
			footstepTimer = footstepInterval;
		}
	}
}
