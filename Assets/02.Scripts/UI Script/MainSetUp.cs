using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSetUp : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("03_EnemyTestFeild 2");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}