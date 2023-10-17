using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void SceneLoaded() {
        ///SceneManager.LoadScene("SampleScene");
        SceneManager.LoadScene("03_EnemyTestFeild");
        Debug.Log("Click");
    }
}
