using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "BrickSettings", menuName = "New Brick Settings", order = 0)]
public class BrickSettings : ScriptableObject
{
    [Range(0.0f, 1.0f)]
    public float powerUpChance = 0.1f;

    [Min(0)]
    public int dropItemAtLeastOnceEvery = 10;

    public GameObject[] powerUps;

    public BrickHealthStatus[] healthStatuses;

    public AudioClip destroyAudio;

    private int streakWithoutPowerUp;

    public Color ColorForHealth(int health) => healthStatuses[health - 1].color;

    public AudioClip ImpactAudioForHealth(int health)
    {
        AudioClip[] audioClips = healthStatuses[health - 1].impactAudios;
        return audioClips[Random.Range(0, audioClips.Length)];
    }

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

    [Serializable]
    public struct BrickHealthStatus
    {
        public Color color;
        public AudioClip[] impactAudios;
    }
}
