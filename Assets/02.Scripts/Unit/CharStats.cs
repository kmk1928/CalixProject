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

    public float t_damage = 1;
    IEnumerator hptext_corutine;  //다시 호출되면 코루틴을 취소하고 다시 생성 3초안에 다시 데미지입으면 
    private float additionalDamage = 0;

    [Header("적이 드랍하는 나노")]
    public int nanoDropAmount = 100;

    private void Awake() {
        curHealth = maxHealth;
    }

    private Coroutine resetCoroutine; // 코루틴을 저장할 변수


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
        // 만약 damageReset 코루틴이 이미 실행 중이라면 중지하고 재시작
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
        }
        resetCoroutine = StartCoroutine(damageReset(additionalDamage)); // damageReset 코루틴 시작
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
