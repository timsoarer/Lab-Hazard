using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject[] enemyPrefabs;
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
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
                spawnPoints[Random.Range(0, spawnPoints.Length)].position,
                Quaternion.identity);
            timer = 0f;
        }
    }
}
