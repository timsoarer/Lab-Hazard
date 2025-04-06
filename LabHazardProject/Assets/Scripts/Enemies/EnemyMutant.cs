using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMutant : MonoBehaviour
{
    [SerializeField]
    private float lungeDistance;
    [SerializeField]
    private float lungeDelay;

    private float timer = 0.0f;
    private Enemy ai;

    void Start()
    {
        ai = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lungeDelay)
        {
            Debug.Log("Executed!");
            timer = 0.0f;
            Vector2 destination;
            if ((ai.GetPlayerPosition() - ai.GetPosition2D()).magnitude > lungeDistance)
            {
                destination = ai.GetPosition2D() + (ai.GetPlayerPosition() - ai.GetPosition2D()).normalized * lungeDistance;
            }
            else
            {
                destination = ai.GetPlayerPosition();
            }
            ai.SetMoveDestination(destination);
        }
    }
}
