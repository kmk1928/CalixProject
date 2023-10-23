using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    //ĳ���͵��� ������ �ִ�. ���⼭ ü�� ������ �����
    [Header("ĳ���͵��� �������� ���� �� �ִ� ����")] 
    public int maxHealth = 1000;
    public float curHealth; // { get; private set; } ���� �ٸ� Ŭ�������� ������ ���ٰ��������� �� ������ ���� Ŭ���������� ����
    public float defense = 0.5f;

    public float attackDamage = 10f;
    public float abillityPower = 10f;

    public bool isDead = false;

    [Header("���� ����ϴ� ����")]
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
