using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float CoinValue;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Movement.Money += CoinValue;           
            Movement.CoinText.text = Movement.Money.ToString() + "c";
            Destroy(gameObject);
        }
    }
}
