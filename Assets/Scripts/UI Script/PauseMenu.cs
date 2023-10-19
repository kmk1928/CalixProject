using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUi;
    [SerializeField] private GameObject background_Img;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(!GameManager.isPause)
                CallMenu();
            else
                CloseMenu();
        }
    }

    private void CallMenu()
    {
        GameManager.isPause = true;
        go_BaseUi.SetActive(true);
        background_Img.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CloseMenu()
    {
        GameManager.isPause = false;
        go_BaseUi.SetActive(false);
        background_Img.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ClickContinue()
    {
        CloseMenu();
    }

    public void ClickSetUp()
    {
        Debug.Log("설정창");
    }

    public void ClickExit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }
}