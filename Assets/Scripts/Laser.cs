using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 5.0f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
    }
}
