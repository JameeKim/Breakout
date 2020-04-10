using UnityEngine;

[CreateAssetMenu(fileName = "BrickSettings", menuName = "New Brick Settings", order = 0)]
public class BrickSettings : ScriptableObject
{
    [Range(0.0f, 1.0f)]
    public float powerUpChance = 0.01f;

    public GameObject[] powerUps;

    public bool RandomPowerUpByChance(out GameObject newGameObject)
    {
        bool shouldBeCreated = Random.Range(0.0f, 1.0f) < powerUpChance;
        newGameObject = shouldBeCreated ? powerUps[Random.Range(0, powerUps.Length)] : null;
        return shouldBeCreated;
    }
}
