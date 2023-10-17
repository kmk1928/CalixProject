using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    //ĳ���͵��� ������ �ִ�. ���⼭ ü�� ������ �����
    [Header("ĳ���͵��� �������� ���� �� �ִ� ����")] 
    public int maxHealth = 1000;
    public float curHealth; // { get; private set; } ���� �ٸ� Ŭ�������� ������ ���ٰ��������� �� ������ ���� Ŭ���������� ����
    public int defense = 5;

    public float attackDamage = 10f;
    public float abillityPower = 10f;

    public bool isDead = false;

    [Header("���� ����ϴ� ����")]
    public int nanoDropAmount = 10;

    private void Awake() {
        curHealth = maxHealth;
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
