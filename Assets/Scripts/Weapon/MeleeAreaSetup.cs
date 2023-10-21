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

    //private void Update() {
    //    if (Input.GetKeyDown(KeyCode.L)) {
    //        OpenDamageCllider_Corutin();
    //    }
    //}

    public void LoadWeaponDamageCollider() {
        //�÷��̾ 1,2,3 ���� ���� ���ҽ�(����) ���� �ݶ��̴� damageCollider�� �޾ƿ�
        //�÷��̾� ��ġ Ȯ��
        damageCollider = playerCtrl.equipWeapon.GetComponent<DamageCollider>();
    }
    public void OpenDamageCllider_Corutin(){       //0.1�ʵ��� ���ݹ��� Ȱ��ȭ
        Debug.Log("�Լ� �ҷ���");
        StartCoroutine(FixedOpenDamageCllider());
    }
    IEnumerator FixedOpenDamageCllider() {
        Debug.Log("���ݹ��� �� �ڷ�ƾ ����");
        damageCollider.EnableMeleeArea();
        yield return new WaitForSeconds(0.3f);
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

    #endregion
 
}
