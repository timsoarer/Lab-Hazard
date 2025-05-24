using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

// ProjectileSide determines who is the "owner" of the projectile.
// Player-made projectiles do not hurt the player, enemy-made projectile do not hurt enemies.
// Neutral projectiles harm both sides
public enum ProjectileSide {
    Player,
    Enemy,
    Neutral
}

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    // Whether or not the particle should interact with players/enemies
    private bool interactWithPlayers;
    private bool interactWithEnemies;

    [SerializeField]
    private Material playerMaterial; // The material for when the projectile is owned by the player
    [SerializeField]
    private Material enemyMaterial; // The material for when the projectile is owned by the enemy
    [SerializeField]
    private Material neutralMaterial; // The material for when the projectile is neutral
    
    // The tints of light sources for each team
    private Color playerLightColor = new Color32(0, 154, 255, 255);
    private Color enemyLightColor = new Color32(255, 9, 0, 255);
    private Color neutralLightColor = new Color32(255, 166, 0, 255); 

    private Light2D light2d; // The light source of the projectile
    private SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    protected float travelAngle = 0f;
    protected Vector2 travelDirection;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        light2d = GetComponent<Light2D>();
        gameObject.tag = "Projectile";
        ChangeProjectileSide(ProjectileSide.Neutral);
        travelDirection = AngleToDirection(travelAngle);
        Init();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Determines whether the object hit is the player, an enemy, or a wall
        if (interactWithPlayers && other.tag == "Player")
        {
            PlayerHealth playerHP = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHP == null)
            {
                Debug.LogError(other.gameObject.name + " has Player tag, but no PlayerHealth.cs script attached!");
            }
            else
            {
                OnPlayerHit(playerHP);
            }
        }
        else if (interactWithEnemies && other.tag == "Enemy")
        {
            Enemy ai = other.gameObject.GetComponent<Enemy>();
            if (ai == null)
            {
                Debug.LogError(other.gameObject.name + " has Enemy tag, but no Enemy.cs script attached!");
            }
            else
            {
                OnEnemyHit(ai);
            }
        }
        else if (other.tag != "Projectile")
        {
            OnWallHit();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    // Switches the owner of the projectile
    public void ChangeProjectileSide(ProjectileSide side)
    {
        switch (side)
        {
            case ProjectileSide.Player:
                spriteRenderer.material = playerMaterial;
                light2d.color = playerLightColor;
                interactWithPlayers = false;
                interactWithEnemies = true;
                break;
            case ProjectileSide.Enemy:
                spriteRenderer.material = enemyMaterial;
                light2d.color = enemyLightColor;
                interactWithPlayers = true;
                interactWithEnemies = false;
                break;
            case ProjectileSide.Neutral:
                spriteRenderer.material = neutralMaterial;
                light2d.color = neutralLightColor;
                interactWithPlayers = true;
                interactWithEnemies = true;
                break;
        }
    }

    // Returns the current travel angle
    public float GetTravelAngle()
    {
        return travelAngle;
    }

    // Takes the angle (in degrees) and changes the travel direction to that angle
    public void SetTravelAngle(float newAngle)
    {
        travelAngle = newAngle;
        travelDirection = AngleToDirection(travelAngle);
    } 

    // Code that runs when the projectile hits a player. Can be overriden.
    public virtual void OnPlayerHit(PlayerHealth playerHP)
    {
        playerHP.Damage();
        Destroy(gameObject);
    }

    // Code that runs when the projectile hits an enemy. Can be overriden.
    public virtual void OnEnemyHit(Enemy enemyAI)
    {
        enemyAI.Damage();
        Destroy(gameObject);
    }

    // Code that runs when the projectile hits a wall. Can be overriden.
    public virtual void OnWallHit()
    {
        Destroy(gameObject);
    }

    // Determines the movement of the projectile. Runs every FixedUpdate(). Can be overrriden.
    public virtual void Move()
    {
        rb.velocity = travelDirection;
    }

    // Initializes any additional variables. Runs during Start() after all the default variables have been initialized.
    public virtual void Init()
    {

    }

    // Turns an angle (in degrees) into a normalized Vector2 pointing at that angle
    private Vector2 AngleToDirection(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}
