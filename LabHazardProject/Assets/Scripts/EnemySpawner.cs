using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private Enemy enemyPrefab;
    [SerializeField]
    private float safeRadius;
    [SerializeField]
    private float spawnDelay = 1f;

    private float timer = 0.0f;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnDelay)
        {
            Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position, Quaternion.identity);
            timer = 0f;
        }
    }
}
