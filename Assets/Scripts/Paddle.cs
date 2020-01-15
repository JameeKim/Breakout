using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float width = 5.0f;
    public float height = 0.3f;
    public GameObject ballSpriteChild;
    public GameObject ballPrefab;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        ApplySize();
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
        Instantiate(
            ballPrefab,
            ballSpriteChild.transform.position,
            Quaternion.identity);
    }

    public void Reset()
    {
        ballSpriteChild.SetActive(true);
        Transform cachedTransform = transform;
        Vector3 newPosition = cachedTransform.localPosition;
        newPosition.x = 0.0f;
        cachedTransform.localPosition = newPosition;
    }
}
