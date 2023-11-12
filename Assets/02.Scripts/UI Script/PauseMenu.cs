using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUi;
    [SerializeField] private GameObject Menu_BackImg;
    [SerializeField] private GameObject SetUp_Menu;
    public Inventory inventory;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel") && !inventory.InventoryActivated)
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
        Menu_BackImg.SetActive(true);
        Time.timeScale = 0.00001f;
    }

    private void CloseMenu()
    {
        GameManager.isPause = false;
        go_BaseUi.SetActive(false);
        Menu_BackImg.SetActive(false);
        SetUp_Menu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ClickContinue()
    {
        CloseMenu();
    }



    public void ClickSetUp()
    {
        go_BaseUi.SetActive(false);
        SetUp_Menu.SetActive(true);
    }

    public void ClickCloseSetUp()
    {
        go_BaseUi.SetActive(true);
        SetUp_Menu.SetActive(false);
    }



    public void ClickExit()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }
}