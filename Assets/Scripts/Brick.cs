using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Brick : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        GameController.Instance.IncrementScore();
    }
}
