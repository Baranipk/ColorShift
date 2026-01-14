using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Hareket Ayarlarý")]
	public float maxSpeed = 12f;      // Ulaþýlacak maksimum hýz
	public float acceleration = 10f;  // Hýzlanma katsayýsý (0-max arasý geçiþ hýzý)
	public float deceleration = 20f;  // Yavaþlama/Durma katsayýsý (Tuþu býrakýnca)
	public float turnSpeed = 15f;     // Ani dönüþ katsayýsý (Saða giderken sola basýnca)

	[Header("Ýnce Ayar")]
	public float velPower = 0.9f;
	[SerializeField] private float jumpForce = 10;

    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;

    public bool isDoubleJump = false;

    [Header("Double Jump Settings")]
    //[SerializeField] private int maxJumpCount = 2; // Kaç kez zýplanabilir? (2 = Double Jump)
    private int _remainingJumps; // Kalan zýplama hakký

    public PlayerInputHandler _playerInputHandler;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private bool _isGrounded;

    private void Start()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }
	public void Move()
	{
		// 1. Input Al
		float moveInput = _playerInputHandler.GetMoveDirection();

		// 2. Hedef Hýzý Hesapla
		// Tuþa basýlýyorsa maxSpeed, basýlmýyorsa 0 hedeflenir.
		float targetSpeed = moveInput * maxSpeed;

		// 3. Hýz Farkýný Bul (Delta)
		// Hedeflediðimiz hýz ile þu anki hýzýmýz arasýndaki fark
		float speedDif = targetSpeed - _rb.linearVelocityX;

		// 4. Hangi Katsayýyý Kullanacaðýz? (Hýzlanma mý, Yavaþlama mý, Dönüþ mü?)
		float accelRate;

		if (Mathf.Abs(targetSpeed) > 0.01f)
		{
			// Hareket ediyoruz...
			// Eðer hedef yön ile þu anki hýz yönü zýtsa (Dönüþ yapýyoruz)
			if (Mathf.Sign(targetSpeed) != Mathf.Sign(_rb.linearVelocityX) && Mathf.Abs(_rb.linearVelocityX) > 0.1f)
			{
				accelRate = turnSpeed; // Dönüþler daha keskin olsun
			}
			else
			{
				accelRate = acceleration; // Normal hýzlanma
			}
		}
		else
		{
			// Tuþa basmýyoruz, durmak istiyoruz
			accelRate = deceleration;
		}

		// 5. Kuvveti Hesapla ve Uygula
		// Fark * HýzlanmaGücü formülü (P-Controller mantýðý gibi çalýþýr)
		// Mathf.Pow kullanýmý hareketin baþlangýcýný daha yumuþak yapar (opsiyoneldir)
		float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

		_rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

		// 6. Görsel Çevirme (Flip)
		if (moveInput != 0)
		{
			_sr.flipX = moveInput < 0;
		}
	}
	public void Jump()
    {             
            _rb.linearVelocityY = 0;
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);
        
    }

    private void OnDrawGizmos()
    {
        if (groundCheckTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
        }
    }
}
