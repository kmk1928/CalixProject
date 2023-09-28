using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [Header("ĳ���͵��� �������� ���� �� �ִ� ����")] 
    public int maxHealth = 100;
    public int curHealth; // { get; private set; } ���� �ٸ� Ŭ�������� ������ ���ٰ��������� �� ������ ���� Ŭ���������� ����
    public int defense = 5;
    public Stat damage;
     
    private void Awake() {
        curHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        curHealth -= damage;
    }

    public void GuardDamage(int damage) {
        curHealth -= (damage - defense);
    }
}
