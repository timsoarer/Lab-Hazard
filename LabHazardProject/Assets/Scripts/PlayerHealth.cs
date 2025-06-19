using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI finalScoreText;
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private AudioClip[] hurtSounds;
    [SerializeField]
    private int maxHp = 3;
    private int hp;
    private RandomSoundPlayer randSoundPlayer;

    private int totalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        PrintHealth();
        randSoundPlayer = GetComponent<RandomSoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int damageValue = 1)
    {
        hp -= damageValue;
        PrintHealth();
        randSoundPlayer.PlayRandomSound(hurtSounds);
        if (hp <= 0)
        {
            deathScreen.SetActive(true);
            finalScoreText.text = "Счёт: " + totalScore.ToString();
            gameObject.SetActive(false);
        }
    }

    public void AddScore(int scoreReward)
    {
        totalScore += scoreReward;
        scoreText.text = "Очки: " + totalScore.ToString();
    }

    private void PrintHealth()
    {
        healthText.text = "Здоровье: [" + new string('|', hp) + new string ('.', maxHp - hp) + ']';
    }
}