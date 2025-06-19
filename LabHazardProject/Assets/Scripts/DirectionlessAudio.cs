using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DirectionlessAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private bool startedPlaying = false;

    public void SetupAudio(AudioClip clip, float volume = 1f)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        startedPlaying = true;
    }

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startedPlaying && !audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
