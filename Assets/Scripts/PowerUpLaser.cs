using UnityEngine;

public class PowerUpLaser : PowerUp
{
    public GameObject laserShooterPrefab;

    protected override void ApplyPowerUp()
    {
        Instantiate(laserShooterPrefab, GameController.Instance.paddle.transform);
    }
}
