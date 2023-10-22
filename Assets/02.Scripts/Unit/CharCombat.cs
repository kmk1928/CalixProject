using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class CharCombat : MonoBehaviour
{
    //캐릭터들의 데미지를 계산한다. 계산한 값을 자신의 CharStats로 넘겨서 체력을 깎음
    CharStats myStats;

    [Header("전투에 관련된 행동 정리")]
    public float atkSpd = 1f;           //공격 속도
    private float atkCdw = 0f;          //공격 딜레이\

    private float finalDamage;

    private void Start() {
        myStats = GetComponent<CharStats>();
    }
    private void Update() {
        atkCdw -= Time.deltaTime;
    }

    #region 피해를 받는 쪽에서 데미지 계산
    //targetStats은 상대방의 스탯이다. 즉 데미지를 자신에게 주는 오브젝트의 스탯 
    public void EnemyHitted(PlayerStats targetStats) {     //공격 데미지를 받음
        finalDamage = (targetStats.attackDamage * targetStats.curDamageCal);
        float randomValue = Random.Range(0f, 1f);
        if(randomValue < targetStats.criticalChance) {
            finalDamage *= targetStats.criticalDamage;
            Debug.Log("Critical!! - CharCombat");
        }
        Debug.LogWarning("적이 받은 데미지" + finalDamage);
        myStats.TakeADDamage(finalDamage);
    }
    public void PlayerHitted(CharStats targetStats) {     //공격 데미지를 받음
        finalDamage = targetStats.attackDamage;
        myStats.TakeADDamage(finalDamage);
    }

    public void Guard(CharStats targetStats) {  //가드 시 데미지 %감소
        finalDamage = targetStats.attackDamage;
        myStats.GuardDamage(finalDamage);
        }

    private void DamageCalculation() {
        /* 임시 데미지 공식
        // float damage = 공격력 * [각인 추가 데미지] * [지속 효과];
        //Random.Range() 함수를 사용하여 0과 1 사이의 랜덤한 값을 생성
        float randomValue = Random.Range(0f, 1f);
        if (randomValue < criticalChance) {
            // 크리티컬 데미지 적용
            damage *= (1 + 크리티컬 데미지);
        }
         */
    }
    #endregion


}




