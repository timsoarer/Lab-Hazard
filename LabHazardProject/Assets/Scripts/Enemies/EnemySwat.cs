using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwat : Enemy
{
    [Header("SWAT Fire Parameters")]
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private Transform muzzlePoint;
    [SerializeField]
    private AudioClip shootSound;

    [Header("SWAT Movement Parameters")]
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

    private Animator anim;
    private SpriteAnimator shootAnim;
    private float moveTimer = 0.0f;
    private float shootTimer = 0.0f;

    public override void Init()
    {
        anim = GetComponent<Animator>();
        shootAnim = GetComponent<SpriteAnimator>();
    }

    public override void UpdateAI()
    {
        moveTimer += Time.deltaTime;
        shootTimer += Time.deltaTime;
        if (moveTimer >= updateFrequency)
        {
            moveTimer = 0.0f;
            SetMoveDestination(CalculateDestination());
        }
        if (shootTimer >= fireRate)
        {
            shootTimer = 0.0f;
            Shoot();
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

    void Shoot()
    {
        Vector2 relativePlayerPos = GetPlayerPosition() - GetPosition2D();
        float angle = Vector2.SignedAngle(Vector2.right, relativePlayerPos);

        GameObject projectileObject = Instantiate(projectilePrefab, muzzlePoint.position, Quaternion.identity);
        Projectile projectileScript = projectileObject.GetComponent<Projectile>();
        if (projectileScript == null)
        {
            Debug.LogError(gameObject.name + " is trying to fire " + projectilePrefab.name + ", but it has no projectile script attached!");
        }
        projectileScript.SetTravelAngle(angle);
        projectileScript.ChangeProjectileSide(ProjectileSide.Enemy);
        AudioSource.PlayClipAtPoint(shootSound, transform.position + Vector3.back * 2);
        shootAnim.ShootAt(relativePlayerPos, 0.2f);
    }
}
