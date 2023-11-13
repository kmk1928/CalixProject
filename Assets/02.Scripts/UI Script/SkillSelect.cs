using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelect : MonoBehaviour
{
    public GameObject skillSelect_UI;
    private bool skillSelect_Active = false;

    [Header("선택된 스킬 이미지 표시")]
    public Image selected_Skill1_img;
    public Image selected_Skill2_img;
    public Image selected_Skill1_img_floating;
    public Image selected_Skill2_img_floating;

    [Header("스킬 이미지 스프라이트")]
    public Sprite rapidAssault_Sp;
    public Sprite flyMech_Sp;
    public Sprite oneSlash_Sp;
    public Sprite bloodRain_Sp;
    private void Awake()
    {
        // DontDestroyOnLoad를 호출하여 이 게임 오브젝트를 씬 전환 시에 파괴되지 않도록 합니다.
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Test스킬선택");
            if (!skillSelect_Active)
            {
                UIManager.isOpenUI = true;
                // L 키를 눌렀을 때 UI를 활성화
                skillSelect_UI.SetActive(true);
                skillSelect_Active = true;
                Time.timeScale = 0.001f;
            }
            else
            {
                UIManager.isOpenUI = false;
                skillSelect_UI.SetActive(false);
                skillSelect_Active = false;
                Time.timeScale = 1f;
            }
        }
    }

    public void Equip_Skill1_RapidAssault()
    {
        selected_Skill1_img.sprite = rapidAssault_Sp;
        selected_Skill1_img_floating.sprite = rapidAssault_Sp;
        SkillManager.isFlyMechEquipped = false;
        SkillManager.isRapidAssaultEquipped = true;
        Debug.Log("RRR");
    }
    public void Equip_Skill1_FlyMech()
    {
        selected_Skill1_img.sprite = flyMech_Sp;
        selected_Skill1_img_floating.sprite = flyMech_Sp;
        SkillManager.isRapidAssaultEquipped = false;
        SkillManager.isFlyMechEquipped = true;
        Debug.Log("FFF");
    }

    public void Equip_Skill2_OneSlash()
    {
        selected_Skill2_img.sprite = oneSlash_Sp;
        selected_Skill2_img_floating.sprite = oneSlash_Sp;
        SkillManager.isBloodRainEquipped = false;
        SkillManager.isOneSlashEquipped = true;
        Debug.Log("OOO");
    }
    public void Equip_Skill2_BloodRain()
    {
        selected_Skill2_img.sprite = bloodRain_Sp;
        selected_Skill2_img_floating.sprite = bloodRain_Sp;
        SkillManager.isOneSlashEquipped = false;
        SkillManager.isBloodRainEquipped = true;
        Debug.Log("BBB");
    }
}
