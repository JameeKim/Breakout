using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    public BrickSettings settings;

    [Min(1)]
    public int maxHealth = 1;

    private SpriteRenderer spriteRenderer;
    private int currentHealth;

    private int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            spriteRenderer.color = settings.ColorForHealth(currentHealth);
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        CurrentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CurrentHealth > 1)
            CurrentHealth--;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (settings.RandomPowerUpByChance(out GameObject powerUp))
            Instantiate(powerUp, transform.position, Quaternion.identity);

        GameController.Instance.IncrementScore();
    }
}
