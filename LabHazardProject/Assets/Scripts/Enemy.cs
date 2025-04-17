using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// Enemy class setting up basic functions to control AI movement
// All enemy classes have to be derived from this class
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;

    private bool isMoving = false;
    private Vector2 moveDestination;

    [SerializeField]
    private float speed = 2.5f; // The speed at which the enemy moves
    [SerializeField]
    private float deadzone = 0.5f; // The minimum distance from the destination at which the enemy stops moving
    [SerializeField]
    private int hp = 1;
    [SerializeField]
    private AudioClip hurtAudio;
    [SerializeField]
    private bool contactDamage = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        gameObject.tag = "Enemy";
        Init();
    }

    void Update()
    {
        UpdateAI();
    }

    void FixedUpdate()
    {
        if (isMoving && (moveDestination - GetPosition2D()).magnitude > deadzone)
        {
            Vector2 direction = (moveDestination - GetPosition2D()).normalized;
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        else
        {
            isMoving = false;
        }
    }

    // Updates the AI with a new move destination that it will move toward
    public void SetMoveDestination(Vector2 destination)
    {
        moveDestination = destination;
        isMoving = true;
    }

    // Gets the position of this enemy as a Vector2
    public Vector2 GetPosition2D()
    {
        return gameObject.transform.position;
    }

    // Gets the position of the player
    public Vector2 GetPlayerPosition()
    {
        return player.transform.position;
    }

    // Initializes any additional variables that derived enemy classes may need. Can be overriden.
    public virtual void Init()
    {
        
    }

    // Runs during Update(), takes care of normal AI behavior
    public virtual void UpdateAI()
    {
        
    }

    public void Damage(int damageValue = 1)
    {
        hp -= damageValue;
        AudioSource.PlayClipAtPoint(hurtAudio, new Vector3(transform.position.x, transform.position.y, -10f));
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
