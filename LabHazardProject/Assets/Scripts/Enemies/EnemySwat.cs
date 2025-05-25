using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwat : Enemy
{
    [Header("SWAT Parameters")]
    [SerializeField]
    private EnemyDetectionRadius detectionRadius;
    [SerializeField]
    private float playerOptimalRadius = 10f;
    [SerializeField]
    private float playerDeadzone = 10f;
    // Coefficient determining how much enemy avoidance should factor into movement compared to following the player
    [SerializeField]
    private float avoidStrength = 1f;
    [SerializeField]
    private float updateFrequency = 0.1f;

    private float timer = 0.0f;

    public override void UpdateAI()
    {
        timer += Time.deltaTime;
        if (timer >= updateFrequency)
        {
            timer = 0.0f;
            SetMoveDestination(CalculateDestination());
        }
    }

    private Vector2 CalculateDestination()
    {
        Vector2 enemyAvoidSum = Vector2.zero;

        foreach (Transform entity in detectionRadius.GetEntitiesInRadius())
        {
            enemyAvoidSum += (Vector2)entity.position;
        }
        enemyAvoidSum -= GetPosition2D() * detectionRadius.GetEntitiesInRadius().Count;
        enemyAvoidSum.Normalize();

        Vector2 playerFollowDir = GetPlayerPosition() - GetPosition2D();

        if (Mathf.Abs(playerFollowDir.magnitude - playerOptimalRadius) < playerDeadzone)
        {
            playerFollowDir = Vector2.zero;
        }
        else if (playerFollowDir.magnitude < playerOptimalRadius)
        {
            playerFollowDir = -playerFollowDir;
            playerFollowDir.Normalize();
        }
        else
        {
            playerFollowDir.Normalize();
        }

        return GetPosition2D() - enemyAvoidSum * avoidStrength + playerFollowDir;
    }
}
