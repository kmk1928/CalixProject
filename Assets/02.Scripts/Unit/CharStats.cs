using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    //캐릭터들의 스탯이 있다. 여기서 체력 증감을 계산함
    [Header("캐릭터들이 공통으로 가질 수 있는 스탯")] 
    public int maxHealth = 1000;
    public float curHealth; // { get; private set; } 쓰면 다른 클래스에서 변수에 접근가능하지만 값 변경은 현재 클래스에서만 가능
    public float defense = 0.5f;

    public float attackDamage = 10f;
    public float abillityPower = 10f;

    public bool isDead = false;

    [Header("적이 드랍하는 나노")]
    public int nanoDropAmount = 100;

    private void Awake() {
        curHealth = maxHealth;
    }

    public void TakeADDamage(float damage) {
        curHealth -= damage;
        DeadCheck();
    }

    public void TakeAPDamage(float damage) {
        curHealth -= damage;
        DeadCheck();
    }

    public void GuardDamage(float damage) {
        curHealth -= (damage * defense);
        DeadCheck();
    }
    private void DeadCheck() {
        if (curHealth <= 0.9f) {
            isDead = true;
            GameManager.instance.AddNano(nanoDropAmount);
        } 
    }

}
