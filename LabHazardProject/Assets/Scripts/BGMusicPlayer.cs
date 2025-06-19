using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicPlayer : MonoBehaviour
{
    private GameObject player;
    private AudioSource audioSource;
    [SerializeField]
    private float deathPitchShiftDelay = 0.2f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.activeSelf)
        {
            timer += Time.deltaTime;
            if (timer >= deathPitchShiftDelay)
            {
                if (audioSource.pitch > 0.1f)
                {
                    audioSource.pitch -= 0.1f;
                }
                else
                {
                    audioSource.Stop();
                }
                timer = 0f;
            }
        }
    }
}
