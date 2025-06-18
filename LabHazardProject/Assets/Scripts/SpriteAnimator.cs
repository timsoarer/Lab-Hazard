using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class SpriteAnimator : MonoBehaviour
{
    public enum Direction
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
    [SerializeField]
    private bool hasShootingAnims = false;
    [SerializeField]
    private bool hasWalkingAnims = false;

    private float overrideTimer = 0f;
    private Direction overridenDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (overrideTimer <= 0f)
        {
            currentDir = GetDirectionFromVector2(rb.velocity);
            if (hasShootingAnims)
            {
                anim.SetBool("Shooting", false);
            }
        }
        else
        {
            currentDir = overridenDir;
            overrideTimer -= Time.deltaTime;
            if (hasShootingAnims)
            {
                anim.SetBool("Shooting", true);
            }
        }

        if (hasWalkingAnims)
        {
            if (rb.velocity.magnitude <= deadzone)
            {
                anim.SetBool("Walking", false);
            }
            else
            {
                anim.SetBool("Walking", true);
            }
        }

        anim.SetInteger("Direction", (int)currentDir);
    }

    public Direction GetDirectionFromVector2(Vector2 input)
    {
        if (input.magnitude < deadzone)
        {
            return currentDir;
        }

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            if (input.x > 0)
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
            if (input.y > 0)
            {
                return Direction.Back;
            }
            else
            {
                return Direction.Front;
            }
        }
    }

    public void ShootAt(Vector2 point, float duration = 1f)
    {
        overridenDir = GetDirectionFromVector2(point);
        overrideTimer = duration;
    }
}
