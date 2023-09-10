using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyMonoBehaviour : MonoBehaviour
{
    [SerializeField] private float coinWorth;
    [SerializeField] private float MaxHealth;
    private float Health;
    public float Damage;
    [SerializeField] private float Speed;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject coin;
    private void Awake()
    {
        Health = MaxHealth;
    }
    void Update()
    {
        transform.position += (player.position - transform.position).normalized * Time.deltaTime * Speed;

        Quaternion rotation = Quaternion.FromToRotation(transform.up, (player.position - transform.position).normalized) * transform.rotation;
        transform.rotation = rotation;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Health -= collision.gameObject.GetComponent<MoveBulet>().damage;
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Image>().fillAmount = Health / MaxHealth;
            Destroy(collision.gameObject);
        }
        if (0 >= Health) 
        {
            GameObject coinCopy = Instantiate(coin);
            coinCopy.GetComponent<Coin>().CoinValue = coinWorth;
            coinCopy.transform.position = transform.position;
            SpawnEnemy.enemyNumber--;
            Destroy(gameObject);
        }
    }
}
