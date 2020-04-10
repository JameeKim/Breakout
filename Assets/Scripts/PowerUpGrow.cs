public class PowerUpGrow : PowerUp
{
    public float duration = 3.0f;

    private Paddle paddle;

    private void Start()
    {
        paddle = GameController.Instance.paddle;
    }

    protected override void ApplyPowerUp()
    {
        paddle.width = paddle.OriginalWidth * 1.25f;
        paddle.ApplySize();
        paddle.ResetWidthAfter(duration);
    }
}
