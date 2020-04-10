using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Floor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            Destroy(other.gameObject);
            GameController.Instance.BallFellDown();
        }
    }
}
