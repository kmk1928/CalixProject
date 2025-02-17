using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAreaSetup : MonoBehaviour
{
    PlayerController playerCtrl;
    DamageCollider damageCollider;      //장착 무기의 damageCollider를 받아오기 위한 선언
    public DamageCollider defaultDamageCollider; 

    private void Start() {
        //장착 무기 콜라이더 받기 위한 플레이어컨트롤러 
        //플레이어 위치 확인
        playerCtrl = GetComponent<PlayerController>();
        LoadDefaultDamageCollider();
    }

    public void LoadWeaponDamageCollider() {
        //플레이어가 1,2,3 으로 무기 스왑시(장착) 무기 콜라이더 damageCollider이 받아옴
        //플레이어 위치 확인
        damageCollider = playerCtrl.equipWeapon.GetComponent<DamageCollider>();
    }
    public void LoadDefaultDamageCollider() {
        //플레이어가 1,2,3 으로 무기 스왑시(장착) 무기 콜라이더 damageCollider이 받아옴
        //플레이어 위치 확인
        damageCollider = defaultDamageCollider;
    }

    public void OpenDamageCollider_Corutin(){       //0.1초동안 공격범위 활성화
        Debug.Log("함수 불러옴");
        StartCoroutine(FixedOpenDamageCollider());
    }
    IEnumerator FixedOpenDamageCollider() {
        Debug.Log("공격범위 온 코루틴 실행");
        damageCollider.EnableMeleeArea();
        yield return new WaitForSeconds(0.2f);
        damageCollider.DisableMeleeArea();
    }

    public void EneyDamageColliderOn() {       //0.1초동안 공격범위 활성화
        StartCoroutine(FixedEnemyOpenDamageCollider());
    }

    IEnumerator FixedEnemyOpenDamageCollider() {
        if (defaultDamageCollider == null) {
            Debug.LogWarning("Enemy 공격 범위 지정 안됨!!!");
            yield break;
        }
        Debug.Log("Enemy 공격 코루틴 실행");
        defaultDamageCollider.EnableMeleeArea();
        yield return new WaitForSeconds(0.08f);
        defaultDamageCollider.DisableMeleeArea();
    }

   

    #region 예전버전

    private void OpenDamageCollider() {         
        //받아온 damageCollider 활성화
        damageCollider.EnableMeleeArea();
    }

    private void CloseDamageCollider() {
        //받아온 damageCollider 비활성화
        damageCollider.DisableMeleeArea();
    }

    #endregion
 
}
