using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileSide {
    Player,
    Enemy,
    Neutral
}

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private bool interactWithPlayers;
    [SerializeField]
    private bool interactWithEnemies;
    [SerializeField]
    private bool interactWithWalls;

    private SpriteRenderer renderer;
    private Rigidbody2D rb;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        gameObject.tag = "Projectile";
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (interactWithPlayers && other.tag == "Player")
        {
            OnPlayerHit(other.gameObject);
        }
        else if (interactWithEnemies && other.tag == "Enemy")
        {
            Enemy ai = other.gameObject.GetComponent<Enemy>();
            if (ai == null)
            {
                Debug.LogError(other.gameObject.name + " has Enemy tag, but no Enemy,cs script attached!");
            }
            else
            {
                OnEnemyHit(ai);
            }
        }
        else if (interactWithWalls && other.tag != "Projectile")
        {
            OnWallHit();
        }
    }

    void Update()
    {
        Move();
    }

    public virtual void OnPlayerHit(GameObject player)
    {
        Debug.Log("Player has been hit by " + gameObject.name);
    }
    public virtual void OnEnemyHit(Enemy other)
    {
        Debug.Log(other.gameObject.name + " has been hit by " + gameObject.name);
    }

    public virtual void OnWallHit()
    {
        Debug.Log(gameObject.name + " hit a wall!");
        Destroy(gameObject);
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(0f, 1f);
    }
}
