using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonoBehaviour : MonoBehaviour
{
    public float Damage;
    [SerializeField] private float Speed;
    [SerializeField] private Transform player;
    void Update()
    {
        transform.position += (player.position - transform.position).normalized * Time.deltaTime * Speed;
    }
}
