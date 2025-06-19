using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempObject : MonoBehaviour
{
    [SerializeField]
    private float lifespan = 1.5f;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifespan)
        {
            Destroy(gameObject);
        }
    }
}
