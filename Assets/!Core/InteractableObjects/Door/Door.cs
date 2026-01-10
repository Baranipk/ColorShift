using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite oppenedSprite;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;



    private Sprite baseSprite;
	private void Awake()
	{
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        baseSprite = spriteRenderer.sprite;
	}
    public async void open()
    {
        spriteRenderer.sprite = oppenedSprite;
        collider.enabled = false;
    }
}
