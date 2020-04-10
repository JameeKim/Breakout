using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectController : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
