using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 10;

    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;

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
        _rb.linearVelocityX = moveSpeed * _playerInputHandler.GetMoveDirection();
        if (_rb.linearVelocityX > 0)
        {
            _sr.flipX = false;
        }
        else if (_rb.linearVelocityX < 0)
        {
            _sr.flipX = true;
        }
        else
        {
             
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
