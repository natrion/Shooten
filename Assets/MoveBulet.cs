using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulet : MonoBehaviour
{
    public float damage;
    public Vector3 movingPosition;
    void Update()
    {
        transform.position += movingPosition;
    }
}
