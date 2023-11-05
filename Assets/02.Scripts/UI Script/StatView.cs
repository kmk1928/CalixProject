using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatView : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats; // Inspector에서 PlayerStats 컴포넌트 할당

    [SerializeField]
    private TextMeshProUGUI AD_Stat;

    [SerializeField]
    private TextMeshProUGUI DF_Stat;

    [SerializeField]
    private TextMeshProUGUI HP_stat;

    void Update()
    {
        if (playerStats != null)
        {
            // PlayerStats 컴포넌트에서 능력치 데이터 가져오기
            float maxHealth = playerStats.maxHealth;
            float attackDamage = playerStats.attackDamage;
            float defense = playerStats.defense;

            // 텍스트 업데이트
            AD_Stat.text = attackDamage.ToString();
            DF_Stat.text = defense.ToString();
            HP_stat.text = maxHealth.ToString();
        }
        else
        {
            Debug.LogWarning("PlayerStats 컴포넌트를 찾을 수 없습니다.");
        }
    }
}





