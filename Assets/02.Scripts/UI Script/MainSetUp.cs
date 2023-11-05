using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSetUp : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("01_StartScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}