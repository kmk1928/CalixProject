using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatView : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    private TextMeshProUGUI AD_Stat;

    [SerializeField]
    private TextMeshProUGUI DF_Stat;

    [SerializeField]
    private TextMeshProUGUI HP_stat;

    [SerializeField]
    private TextMeshProUGUI AD_StatBonus1;

    [SerializeField]
    private TextMeshProUGUI DF_StatBonus1;

    [SerializeField]
    private TextMeshProUGUI HP_StatBonus1;

    [SerializeField]
    private TextMeshProUGUI AD_StatBonus2;

    [SerializeField]
    private TextMeshProUGUI DF_StatBonus2;

    [SerializeField]
    private TextMeshProUGUI HP_StatBonus2;

    void Update()
    {
        if (playerStats != null)
        {
            // PlayerStats 컴포넌트에서 능력치 데이터 가져오기
            float maxHealth = playerStats.maxHealth;
            float attackDamage = playerStats.attackDamage;
            float defense = playerStats.defense;

            int redCount = playerStats.redCount;
            int yellowCount = playerStats.yellowCount;
            int blueCount = playerStats.blueCount;

            // 텍스트 업데이트
            AD_Stat.text = attackDamage.ToString();
            DF_Stat.text = defense.ToString();
            HP_stat.text = maxHealth.ToString();

            AD_statBonus1();
            AD_statBonus2();
            DF_statBonus1();
            DF_statBonus2();
            HP_statBonus1();
            HP_statBonus2();
        }
        else
        {
            Debug.LogWarning("PlayerStats 컴포넌트를 찾을 수 없습니다.");
        }
    }

    public void AD_statBonus1()
    {
        if (playerStats.yellowCount >= 3)
            AD_StatBonus1.enabled = true;
        else
            AD_StatBonus1.enabled = false;
    }

    public void AD_statBonus2()
    {
        if (playerStats.yellowCount >= 6)
            AD_StatBonus2.enabled = true;
        else
            AD_StatBonus2.enabled = false;
    }

    public void DF_statBonus1()
    {
        if (playerStats.blueCount >= 3)
            DF_StatBonus1.enabled = true;
        else
            DF_StatBonus1.enabled = false;
    }

    public void DF_statBonus2()
    {
        if (playerStats.blueCount >= 6)
            DF_StatBonus2.enabled = true;
        else
            DF_StatBonus2.enabled = false;
    }

    public void HP_statBonus1()
    {
        if (playerStats.redCount >= 3)
            HP_StatBonus1.enabled = true;
        else
            HP_StatBonus1.enabled = false;
    }

    public void HP_statBonus2()
    {
        if (playerStats.redCount >= 6)
            HP_StatBonus2.enabled = true;
        else
            HP_StatBonus2.enabled = false;
    }
}










