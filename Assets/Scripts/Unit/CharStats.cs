using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [Header("캐릭터들이 공통으로 가질 수 있는 스탯")] 
    public int maxHealth = 1000;
    public float curHealth; // { get; private set; } 쓰면 다른 클래스에서 변수에 접근가능하지만 값 변경은 현재 클래스에서만 가능
    public int defense = 5;

    public float attackDamage = 10f;
    public float abillityPower = 10f;

    [Header("플레이어용 치명타 스탯")]
    [Tooltip("치명타 확률 0.0~1.0")]
    public float criticalChance = 0.5f; // 20%의 크리티컬 확률
    [Tooltip("치명타 피해 증가량 기본 1.5배")]
    public float criticalDamage = 1.5f;
    



    private void Awake() {
        curHealth = maxHealth;
    }

    private void Update() {
        
    }

    public void TakeADDamage(float damage) {
        curHealth -= damage;
    }

    public void TakeAPDamage(float damage) {
        curHealth -= damage;
    }

    public void GuardDamage(float damage) {
        curHealth -= (damage - defense);
    }
}
