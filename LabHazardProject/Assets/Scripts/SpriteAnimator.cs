using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class SpriteAnimator : MonoBehaviour
{
    private enum Direction
    {
        Front,
        Back,
        Left,
        Right
    }

    private Direction currentDir = Direction.Right;
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField]
    private float deadzone = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentDir = GetDirectionFromVelocity();
        anim.SetInteger("Direction", (int)currentDir);
    }

    Direction GetDirectionFromVelocity()
    {
        if (rb.velocity.magnitude < deadzone)
        {
            return currentDir;
        }

        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
        {
            if (rb.velocity.x > 0)
            {
                return Direction.Right;
            }
            else
            {
                return Direction.Left;
            }
        }
        else
        {
            if (rb.velocity.y > 0)
            {
                return Direction.Back;
            }
            else
            {
                return Direction.Front;
            }
        }
    }
}
