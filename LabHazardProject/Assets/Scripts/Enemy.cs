using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
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

    [Header("General Parameters")]
    [SerializeField]
    private float speed = 2.5f; // The speed at which the enemy moves
    [SerializeField]
    private float deadzone = 0.5f; // The minimum distance from the destination at which the enemy stops moving
    [SerializeField]
    private int hp = 1;
    [SerializeField]
    private int pointsAwarded = 100;

    [Header("Contact Damage")]
    [SerializeField]
    private bool doesContactDamage = true;
    [SerializeField]
    private int contactDamageValue = 1;
    [SerializeField]
    private float contactDamageCooldown = 0.5f;
    private float contactTimer = 0.0f;


    [Header("Audio and Visuals")]
    [SerializeField]
    private AudioClip hurtAudio;
    [SerializeField]
    private GameObject popupPrefab;
    

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
        if (doesContactDamage)
        {
            contactTimer += Time.deltaTime;
        }
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
        AudioSource.PlayClipAtPoint(hurtAudio, new Vector3(transform.position.x, transform.position.y, -7f));
        if (hp <= 0)
        {
            GameObject popup = Instantiate(popupPrefab);
            popup.GetComponent<ScorePopup>().SetPopupValue(transform.position, "+" + pointsAwarded.ToString());
            player.GetComponent<PlayerHealth>().AddScore(pointsAwarded);
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (doesContactDamage && contactTimer >= contactDamageCooldown && col.collider.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().Damage(contactDamageValue);
            contactTimer = 0f;
        }
    }

    public bool HasDestination() => isMoving;

    // Gets the position of this enemy as a Vector2
    public Vector2 GetPosition2D()
    {
        return gameObject.transform.position;
    }

    // Gets the position of the player
    public Vector2 GetPlayerPosition()
    {
        if (!player) return new Vector2(Random.Range(-100, 100), Random.Range(-100, 100)); // Randomly wander if player is dead
        return player.transform.position;
    }
}
