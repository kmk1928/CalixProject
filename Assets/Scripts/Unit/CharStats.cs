using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [Header("ĳ���͵��� �������� ���� �� �ִ� ����")] 
    public int maxHealth = 100;
    public float curHealth; // { get; private set; } ���� �ٸ� Ŭ�������� ������ ���ٰ��������� �� ������ ���� Ŭ���������� ����
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
