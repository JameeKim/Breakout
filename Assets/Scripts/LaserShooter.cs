using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public int totalTimesToShoot = 10;

    public GameObject laserPrefab;

    private int timesLeft;

    public bool IsAttachedToPaddle { get; set; }

    private void Awake()
    {
        timesLeft = totalTimesToShoot;
    }

    private void Start()
    {
        GameController.Instance.paddle.SetLaserShooter(this);
        SetLaserCountText();
    }

    private void Update()
    {
        if (!Input.GetButtonDown("Action"))
            return;

        ShootLaser();
        timesLeft--;
        SetLaserCountText();
        if (timesLeft == 0)
            GameController.Instance.paddle.SetLaserShooter(null);
    }

    public void RefillLasers()
    {
        timesLeft = totalTimesToShoot;
        SetLaserCountText();
    }

    private void ShootLaser()
    {
        Instantiate(laserPrefab, transform.position, Quaternion.identity);
    }

    private void SetLaserCountText()
    {
        GameController.Instance.laserCountText.text = timesLeft.ToString();
    }
}
