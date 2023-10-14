using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAreaSetup : MonoBehaviour
{
    PlayerController playerCtrl;
    DamageCollider damageCollider;      //���� ������ damageCollider�� �޾ƿ��� ���� ����

    private void Start() {
        //���� ���� �ݶ��̴� �ޱ� ���� �÷��̾���Ʈ�ѷ� 
        playerCtrl = GetComponentInParent<PlayerController>();
    }

    public void LoadWeaponDamageCollider() {        
        //�÷��̾ 1,2,3 ���� ���� ���ҽ�(����) ���� �ݶ��̴� damageCollider�� �޾ƿ�
        damageCollider = playerCtrl.equipWeapon.GetComponent<DamageCollider>();
    }


    private void OpenDamageCollider() {         
        //�޾ƿ� damageCollider Ȱ��ȭ
        damageCollider.EnableMeleeArea();
    }

    private void CloseDamageCollider() {
        //�޾ƿ� damageCollider ��Ȱ��ȭ
        damageCollider.DisableMeleeArea();
    }


}
