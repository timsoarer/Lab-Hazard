using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDetectionRadius : MonoBehaviour
{
    private List<Transform> entitiesInRadius = new List<Transform>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            entitiesInRadius.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            entitiesInRadius.Remove(other.transform);
        }
    }

    public List<Transform> GetEntitiesInRadius()
    {
        entitiesInRadius.RemoveAll(x => !x); // Removes all null values from list
        return entitiesInRadius;
    }
}
