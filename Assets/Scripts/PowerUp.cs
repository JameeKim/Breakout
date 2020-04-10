using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public abstract class PowerUp : MonoBehaviour
{
    public float speed = 10.0f;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Paddle"))
        {
            ApplyPowerUp();
            Destroy(gameObject);
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }

    protected void Update()
    {
        Transform transformTmp = transform;
        Vector3 position = transformTmp.position;
        position.y -= speed * Time.deltaTime;
        transformTmp.position = position;
    }

    protected abstract void ApplyPowerUp();
}
