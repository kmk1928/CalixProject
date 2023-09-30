using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [Header("캐릭터들이 공통으로 가질 수 있는 스탯")] 
    public int maxHealth = 100;
    public float curHealth; // { get; private set; } 쓰면 다른 클래스에서 변수에 접근가능하지만 값 변경은 현재 클래스에서만 가능
    public int defense = 5;

    public float attackDamage = 10f;
    public float abillityPower = 10f;

    public int defaultDamage = 10;

    public int nano = 100;

     
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
