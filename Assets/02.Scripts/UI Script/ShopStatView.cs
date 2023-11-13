using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopStatView : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    private TextMeshProUGUI AD_ShopStat;

    [SerializeField]
    private TextMeshProUGUI DF_ShopStat;

    [SerializeField]
    private TextMeshProUGUI HP_Shopstat;


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
            AD_ShopStat.text = attackDamage.ToString();
            DF_ShopStat.text = defense.ToString();
            HP_Shopstat.text = maxHealth.ToString();
        }
    }
}
