using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [Header("ĳ���͵��� �������� ���� �� �ִ� ����")] 
    public int maxHealth = 1000;
    public float curHealth; // { get; private set; } ���� �ٸ� Ŭ�������� ������ ���ٰ��������� �� ������ ���� Ŭ���������� ����
    public int defense = 5;

    public float attackDamage = 10f;
    public float abillityPower = 10f;

    [Header("�÷��̾�� ġ��Ÿ ����")]
    [Tooltip("ġ��Ÿ Ȯ�� 0.0~1.0")]
    public float criticalChance = 0.5f; // 20%�� ũ��Ƽ�� Ȯ��
    [Tooltip("ġ��Ÿ ���� ������ �⺻ 1.5��")]
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
