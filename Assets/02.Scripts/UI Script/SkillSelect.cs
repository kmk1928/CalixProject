using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
    public GameObject skillSelect_UI;
    private bool skillSelect_Active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Test��ų����");
            if (!skillSelect_Active)
            {
                // L Ű�� ������ �� UI�� Ȱ��ȭ
                skillSelect_UI.SetActive(true);
                skillSelect_Active = true;
                Time.timeScale = 0f;
            }
            else
            {
                skillSelect_UI.SetActive(false);
                skillSelect_Active = false;
                Time.timeScale = 1f;
            }
        }
    }
}
