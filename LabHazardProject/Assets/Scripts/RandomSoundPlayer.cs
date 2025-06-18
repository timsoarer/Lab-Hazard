using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField]
    private DirectionlessAudio directionlessPrefab;

    public void PlayRandomSoundAtPoint(Vector2 point, AudioClip[] randomSounds)
    {
        AudioSource.PlayClipAtPoint(randomSounds[Random.Range(0, randomSounds.Length)],
            new Vector3(point.x, point.y, 0f));
    }

    public void PlayRandomSound(AudioClip[] randomSounds, float volume = 1f)
    {
        DirectionlessAudio source = Instantiate(directionlessPrefab);
        source.SetupAudio(randomSounds[Random.Range(0, randomSounds.Length)], volume);
    }
}
