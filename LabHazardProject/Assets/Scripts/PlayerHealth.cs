using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject deathText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private AudioClip[] hurtSounds;
    [SerializeField]
    private int hp = 3;
    private RandomSoundPlayer randSoundPlayer;

    [SerializeField]
    private AudioSource musicPlayer;

    private int totalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        randSoundPlayer = GetComponent<RandomSoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damageValue = 1)
    {
        hp -= damageValue;
        randSoundPlayer.PlayRandomSound(hurtSounds);
        if (hp <= 0)
        {
            deathText.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void AddScore(int scoreReward)
    {
        totalScore += scoreReward;
        scoreText.text = "Очки: " + totalScore.ToString();
    }
}