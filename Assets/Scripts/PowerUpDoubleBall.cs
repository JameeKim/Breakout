public class PowerUpDoubleBall : PowerUp
{
    protected override void ApplyPowerUp()
    {
        GameController.Instance.BallManager.Split();
    }
}
