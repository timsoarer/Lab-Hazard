using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMutant : Enemy
{
    [Header("Mutant Parameters")]
    [SerializeField]
    private float lungeDistance;
    [SerializeField]
    private float lungeDelay;
    [SerializeField]
    private AudioClip[] groanSounds;
    [SerializeField]
    private float groanLowDelay = 2f;
    [SerializeField]
    private float groanHighDelay = 5f;
    private float currentGroanDelay = 0f;

    private float timer = 0.0f;
    private float groanTimer = 0.0f;

    // Update is called once per frame
    public override void UpdateAI()
    {
        timer += Time.deltaTime;
        groanTimer += Time.deltaTime;
        if (timer >= lungeDelay)
        {
            timer = 0.0f;
            Vector2 destination;
            if ((GetPlayerPosition() - GetPosition2D()).magnitude > lungeDistance)
            {
                destination = GetPosition2D() + (GetPlayerPosition() - GetPosition2D()).normalized * lungeDistance;
            }
            else
            {
                destination = GetPlayerPosition();
            }
            SetMoveDestination(destination);
        }
        if (groanTimer >= currentGroanDelay)
        {
            groanTimer = 0.0f;
            randSoundPLayer.PlayRandomSoundAtPoint(transform.position, groanSounds);
            currentGroanDelay = Random.Range(groanLowDelay, groanHighDelay);
        }
    }
}