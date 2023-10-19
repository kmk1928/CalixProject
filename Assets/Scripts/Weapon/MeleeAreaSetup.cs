using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAreaSetup : MonoBehaviour
{
    PlayerController playerCtrl;
    DamageCollider damageCollider;      //���� ������ damageCollider�� �޾ƿ��� ���� ����

    private void Start() {
        //���� ���� �ݶ��̴� �ޱ� ���� �÷��̾���Ʈ�ѷ� 
        //�÷��̾� ��ġ Ȯ��
        playerCtrl = GetComponent<PlayerController>();
    }

    public void LoadWeaponDamageCollider() {
        //�÷��̾ 1,2,3 ���� ���� ���ҽ�(����) ���� �ݶ��̴� damageCollider�� �޾ƿ�
        //�÷��̾� ��ġ Ȯ��
        damageCollider = playerCtrl.equipWeapon.GetComponent<DamageCollider>();
    }
    IEnumerator FixedOpenDamageCllider() {
        damageCollider.EnableMeleeArea();
        yield return new WaitForSeconds(0.1f);
        damageCollider.DisableMeleeArea();
    }

    #region ��������

    private void OpenDamageCollider() {         
        //�޾ƿ� damageCollider Ȱ��ȭ
        damageCollider.EnableMeleeArea();
    }

    private void CloseDamageCollider() {
        //�޾ƿ� damageCollider ��Ȱ��ȭ
        damageCollider.DisableMeleeArea();
    }

    private void OpenDamageCllider_Corutin() {
        StartCoroutine(FixedOpenDamageCllider());
    }
    #endregion
 
}
