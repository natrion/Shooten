using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnEnemy : MonoBehaviour
{
    public static int WaveNumber = 0;
    [SerializeField] private AudioSource Music;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform[] Enemy;
    [SerializeField] private  GameObject BuyMenu;
    [SerializeField] private GameObject[] BuyButtons;
    [SerializeField] private TextMeshProUGUI RoundText;
    public static int Difficulty;
    public static int enemyNumber;
    public void QuitScene()
    {
        SceneManager.LoadScene("Menu");
    }
    void Start()
    {
        StartCoroutine(SpawnEnemys());
    }
    private bool buyMenu;
    public void CloseBuyMenu()
    {
        buyMenu = false;
    }
    private IEnumerator SpawnEnemys()
    {
        WaveNumber = 1;
        bool DoSpawn = true;
        while (DoSpawn == true)
        {
            enemyNumber = 0;
            for (int i = 0; i < WaveNumber * Difficulty; i++)
            {
                enemyNumber++;
                GameObject enemy = Instantiate(Enemy[Random.Range(0 , Enemy.Length)].gameObject);
                enemy.transform.position =  Player.transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                enemy.SetActive(true);
                yield return new WaitForSeconds(1);
            }
            
            
            while (enemyNumber>0)
            {
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(2);
            WaveNumber++;
            RoundText.text = "Round:" + WaveNumber.ToString();
            BuyMenu.SetActive(true);
            GameObject Button1 = Instantiate(BuyButtons[Random.Range(0, BuyButtons.Length - 1)]);
            Button1.GetComponent<RectTransform>().position = new Vector3(Screen.width - 300, Screen.height - 150, 0);
            Button1.transform.parent = BuyMenu.transform;
            GameObject Button2 = Instantiate(BuyButtons[Random.Range(0, BuyButtons.Length - 1)]);
            Button2.GetComponent<RectTransform>().position = new Vector3(Screen.width / 2, Screen.height - 150, 0);
            Button2.transform.parent = BuyMenu.transform;
            GameObject Button3 = Instantiate(BuyButtons[Random.Range(0, BuyButtons.Length - 1)]);
            Button3.transform.parent = BuyMenu.transform;
            Button3.GetComponent<RectTransform>().position = new Vector3(0 + 300, Screen.height - 150, 0);


            buyMenu = true;
            Time.timeScale = 0f;
            Player.GetComponent<Movement>().UpdateCost();

            Music.pitch = 0.5f;
            while (buyMenu == true)
            {
                yield return null;
            }
            Music.pitch = 1f;
            Time.timeScale = 1f;
            Destroy(Button1);
            Destroy(Button2);
            Destroy(Button3);
            BuyMenu.SetActive(false);
        }
    }
    
}
