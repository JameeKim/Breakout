using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Floor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            GameController.Instance.BallFellDown();
        }
    }
}
