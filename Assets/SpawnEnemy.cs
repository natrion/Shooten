using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]private Transform Player;
    [SerializeField] private Transform Enemy;
    void Start()
    {
        StartCoroutine(SpawnEnemys());
    } 
    private IEnumerator SpawnEnemys()
    {
        bool DoSpawn = true;
        int Round = 0;
        while (DoSpawn == true)
        {
            for (int i = 0; i < Round * 4; i++)
            {
                GameObject enemy = Instantiate(Enemy.gameObject);
                enemy.transform.position =  Player.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            }
            yield return new WaitForSeconds(10);
        }
    }
}
