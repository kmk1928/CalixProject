using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharStats
{

    [Header("플레이어용 치명타 스탯")]
    [Tooltip("치명타 확률 0.0~1.0")]
    public float criticalChance = 0.5f; // 50%의 크리티컬 확률
    [Tooltip("치명타 피해 증가량 기본 1.5배")]
    public float criticalDamage = 1.5f;


}
