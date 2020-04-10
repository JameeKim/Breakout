using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class Brick : MonoBehaviour
{
    public BrickSettings settings;

    [Min(1)]
    public int maxHealth = 1;

    private static int currentNumber;

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

    private void Start()
    {
        currentNumber++;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioClip sound;

        if (CurrentHealth > 1)
        {
            sound = settings.ImpactAudioForHealth(CurrentHealth);
            CurrentHealth--;
        }
        else
        {
            sound = settings.destroyAudio;
            if (settings.RandomPowerUpByChance(out GameObject powerUp))
                Instantiate(powerUp, transform.position, Quaternion.identity);
            GameController.Instance.IncrementScore();
            currentNumber--;
            Destroy(gameObject);
        }

        GameController.Instance.brickSounds.PlayOneShot(sound);
        if (currentNumber == 0)
            GameController.Instance.GoToNextLevel();
    }

    public static void ResetCount()
    {
        currentNumber = 0;
    }
}
