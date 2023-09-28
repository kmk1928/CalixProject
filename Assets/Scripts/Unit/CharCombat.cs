using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class CharCombat : MonoBehaviour
{
    [Header("전투에 관련된 행동 정리")]
    public float atkSpd = 1f;           //공격 속도
    private float atkCdw = 0f;          //공격 딜레이

    CharStats myStats;

    private void Start() {
        myStats = GetComponent<CharStats>();
    }

    private void Update() {
        atkCdw -= Time.deltaTime;
    }

    public void Attack(CharStats targetStats) {
        if(atkCdw <= 0) {
            targetStats.TakeDamage(myStats.damage.GetStat());
            atkCdw = 1f / atkSpd;
        }
    }
}
