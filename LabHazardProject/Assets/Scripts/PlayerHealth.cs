using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject deathText;
    [SerializeField]
    private AudioClip hurtAudio;
    [SerializeField]
    private int hp = 3;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damageValue = 1)
    {
        hp -= damageValue;
        AudioSource.PlayClipAtPoint(hurtAudio, Camera.main.transform.position);
        Debug.Log("Playing audio!");
        if (hp <= 0)
        {
            deathText.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
