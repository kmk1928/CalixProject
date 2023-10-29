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

    public float t_damage = 1;
    IEnumerator hptext_corutine;  //�ٽ� ȣ��Ǹ� �ڷ�ƾ�� ����ϰ� �ٽ� ���� 3�ʾȿ� �ٽ� ������������ 
    private float additionalDamage = 0;

    [Header("���� ����ϴ� ����")]
    public int nanoDropAmount = 100;

    private void Awake() {
        curHealth = maxHealth;
    }

    private Coroutine resetCoroutine; // �ڷ�ƾ�� ������ ����


    IEnumerator damageReset(float additionalDamage)
    {
        t_damage += additionalDamage;
        yield return new WaitForSeconds(3f);
        t_damage = 0;
    }

    public void TakeADDamage(float damage) {
        curHealth -= damage;
        additionalDamage = damage;
        DeadCheck();
        // ���� damageReset �ڷ�ƾ�� �̹� ���� ���̶�� �����ϰ� �����
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
        }
        resetCoroutine = StartCoroutine(damageReset(additionalDamage)); // damageReset �ڷ�ƾ ����
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
