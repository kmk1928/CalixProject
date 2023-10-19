using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAreaSetup : MonoBehaviour
{
    PlayerController playerCtrl;
    DamageCollider damageCollider;      //장착 무기의 damageCollider를 받아오기 위한 선언

    private void Start() {
        //장착 무기 콜라이더 받기 위한 플레이어컨트롤러 
        //플레이어 위치 확인
        playerCtrl = GetComponent<PlayerController>();
    }

    public void LoadWeaponDamageCollider() {
        //플레이어가 1,2,3 으로 무기 스왑시(장착) 무기 콜라이더 damageCollider이 받아옴
        //플레이어 위치 확인
        damageCollider = playerCtrl.equipWeapon.GetComponent<DamageCollider>();
    }
    IEnumerator FixedOpenDamageCllider() {
        damageCollider.EnableMeleeArea();
        yield return new WaitForSeconds(0.1f);
        damageCollider.DisableMeleeArea();
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

    private void OpenDamageCllider_Corutin() {
        StartCoroutine(FixedOpenDamageCllider());
    }
    #endregion
 
}
