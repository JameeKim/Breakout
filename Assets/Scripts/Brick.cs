using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Brick : MonoBehaviour
{
    public BrickSettings settings;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (settings.RandomPowerUpByChance(out GameObject powerUp))
            Instantiate(powerUp, transform.position, Quaternion.identity);
        GameController.Instance.IncrementScore();
        Destroy(gameObject);
    }
}
