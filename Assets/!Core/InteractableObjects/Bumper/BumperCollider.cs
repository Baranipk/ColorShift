using UnityEngine;

public class BumperCollider : MonoBehaviour
{
    public int Dir = 0;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Dir = 1;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		Dir = 0;
	}
}
