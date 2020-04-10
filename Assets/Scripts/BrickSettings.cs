using UnityEngine;

[CreateAssetMenu(fileName = "BrickSettings", menuName = "New Brick Settings", order = 0)]
public class BrickSettings : ScriptableObject
{
    [Range(0.0f, 1.0f)]
    public float powerUpChance = 0.1f;

    [Min(0)]
    public int dropItemAtLeastOnceEvery = 10;

    public GameObject[] powerUps;

    private int streakWithoutPowerUp;

    public bool RandomPowerUpByChance(out GameObject newGameObject)
    {
        bool shouldBeCreated = Random.Range(0.0f, 1.0f) < powerUpChance;
        newGameObject = shouldBeCreated ? PickRandomPowerUp() : null;

        if (dropItemAtLeastOnceEvery == 0)
            return shouldBeCreated;

        if (shouldBeCreated)
        {
            streakWithoutPowerUp = 0;
            return true;
        }

        streakWithoutPowerUp++;

        if (streakWithoutPowerUp < dropItemAtLeastOnceEvery)
            return false;

        newGameObject = PickRandomPowerUp();
        streakWithoutPowerUp = 0;
        return true;
    }

    private GameObject PickRandomPowerUp() => powerUps[Random.Range(0, powerUps.Length)];
}
