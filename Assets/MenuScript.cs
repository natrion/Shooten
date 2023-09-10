using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
