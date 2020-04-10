using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Paddle : MonoBehaviour
{
    public float width = 5.0f;
    public float height = 0.3f;
    public GameObject ballSpriteChild;
    public GameObject ballPrefab;
    public AudioClip ballShootSound;

    [Header("PowerUp - Grow")]
    public AudioClip growSound;

    public AudioClip shrinkSound;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Power Up - Grow
    private float originalWidth;
    private Coroutine currentWidthReturnCoroutine;
    public float OriginalWidth => originalWidth;

    // Power Up - Laser
    private LaserShooter laserShooter;
    public bool HasLaser => laserShooter != null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        originalWidth = width;
        Reset();
    }

    public void ApplySize()
    {
        spriteRenderer.size = new Vector2(width, height);
        boxCollider.size = new Vector2(width, height);
    }

    public void ShootBall()
    {
        if (!ballSpriteChild.activeSelf)
        {
            return;
        }

        ballSpriteChild.SetActive(false);
        Instantiate(ballPrefab, ballSpriteChild.transform.position, Quaternion.identity);
        GameController.Instance.miscSounds.PlayOneShot(ballShootSound);
    }

    public void Reset()
    {
        ballSpriteChild.SetActive(true);
        Transform cachedTransform = transform;
        Vector3 newPosition = cachedTransform.localPosition;
        newPosition.x = 0.0f;
        cachedTransform.localPosition = newPosition;
        SetLaserShooter(null);
        if (currentWidthReturnCoroutine != null)
            StopCoroutine(currentWidthReturnCoroutine);
        ResetSize();
    }

    public void ResetWidthAfter(float duration)
    {
        if (currentWidthReturnCoroutine != null)
            StopCoroutine(currentWidthReturnCoroutine);

        currentWidthReturnCoroutine = StartCoroutine(ReturnWidthCoroutine(duration));
    }

    private IEnumerator ReturnWidthCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        ResetSize();
        GameController.Instance.powerUpSounds.PlayOneShot(shrinkSound);
        currentWidthReturnCoroutine = null;
    }

    private void ResetSize()
    {
        width = originalWidth;
        ApplySize();
    }

    public void SetLaserShooter(LaserShooter shooter)
    {
        if (!HasLaser)
        {
            if (shooter == null)
                return;

            laserShooter = shooter;
            GameController.Instance.laserCountUI.SetActive(true);
            laserShooter.IsAttachedToPaddle = true;

            return;
        }

        if (shooter != null)
        {
            laserShooter.RefillLasers();
            Destroy(shooter.gameObject);

            return;
        }

        Destroy(laserShooter);
        laserShooter = null;
        GameController.Instance.laserCountUI.SetActive(false);
    }
}
