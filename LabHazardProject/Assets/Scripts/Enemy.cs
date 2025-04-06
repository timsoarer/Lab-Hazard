using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;

    private bool isMoving = false;
    private Vector2 moveDestination;

    [SerializeField]
    private float speed = 2.5f;
    [SerializeField]
    private float deadzone = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        SetMoveDestination(Vector2.zero);
    }

    void FixedUpdate()
    {
        if (isMoving && (moveDestination - GetPosition2D()).magnitude > deadzone)
        {
            Vector2 direction = (moveDestination - GetPosition2D()).normalized;
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
            Debug.Log("Still moving!");
        }
        else
        {
            Debug.Log("Stopped!");
            isMoving = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(moveDestination.x, moveDestination.y, 1.0f), deadzone);
    } 

    public void SetMoveDestination(Vector2 destination)
    {
        moveDestination = destination;
        isMoving = true;
    }

    public Vector2 GetPosition2D()
    {
        return gameObject.transform.position;
    }

    public Vector2 GetPlayerPosition()
    {
        Debug.Log(player.name);
        Debug.Log(player.transform.position);
        return player.transform.position;
    }
}
