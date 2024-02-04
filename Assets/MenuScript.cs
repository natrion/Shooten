using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public void StartGame(int difficulty)
    {
        SceneManager.LoadScene("Game");
        SpawnEnemy.Difficulty = difficulty;
    }
    public void Quit()
    {
        Application.Quit();
    }
    [SerializeField]private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI moneyText;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            Movement.Money = PlayerPrefs.GetFloat("Money");
        }
        else
        {
            Movement.Money = 0;
            PlayerPrefs.SetFloat("Money", 0) ;
        }
        moneyText.text = Movement.Money.ToString();

        if (PlayerPrefs.HasKey("score"))
        {
            highScore.text = PlayerPrefs.GetFloat("score").ToString();
        }
        else
        {
            highScore.text = "0";
        }    
    }
}
