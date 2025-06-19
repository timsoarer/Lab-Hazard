using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Transform objectToFollow;

    // Update is called once per frame
    void Update()
    {
        if (!objectToFollow) Destroy(this);
        transform.position = objectToFollow.position;
    }
}
