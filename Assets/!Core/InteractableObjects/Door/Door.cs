using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite oppenedSprite;
    private SpriteRenderer spriteRenderer;
    private Collider2D colliders;



    private Sprite baseSprite;
	private void Awake()
	{
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliders = GetComponent<Collider2D>();
        baseSprite = spriteRenderer.sprite;
	}
    public void open()
    {
        spriteRenderer.sprite = oppenedSprite;
        colliders.enabled = false;
    }
}
