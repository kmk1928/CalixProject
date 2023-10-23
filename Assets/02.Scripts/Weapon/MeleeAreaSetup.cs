using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAreaSetup : MonoBehaviour
{
    PlayerController playerCtrl;
    DamageCollider damageCollider;      //���� ������ damageCollider�� �޾ƿ��� ���� ����
    public DamageCollider defaultDamageCollider;

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
    public void LoadDefaultDamageCollider() {
        //�÷��̾ 1,2,3 ���� ���� ���ҽ�(����) ���� �ݶ��̴� damageCollider�� �޾ƿ�
        //�÷��̾� ��ġ Ȯ��
        damageCollider = defaultDamageCollider;
    }

    public void OpenDamageCollider_Corutin(){       //0.1�ʵ��� ���ݹ��� Ȱ��ȭ
        Debug.Log("�Լ� �ҷ���");
        StartCoroutine(FixedOpenDamageCollider());
    }
    IEnumerator FixedOpenDamageCollider() {
        Debug.Log("���ݹ��� �� �ڷ�ƾ ����");
        damageCollider.EnableMeleeArea();
        yield return new WaitForSeconds(0.2f);
        damageCollider.DisableMeleeArea();
    }

    public void EneyDamageColliderOn() {       //0.1�ʵ��� ���ݹ��� Ȱ��ȭ
        StartCoroutine(FixedEnemyOpenDamageCollider());
    }

    IEnumerator FixedEnemyOpenDamageCollider() {
        if (defaultDamageCollider == null) {
            Debug.LogWarning("Enemy ���� ���� ���� �ȵ�!!!");
            yield break;
        }
        Debug.Log("Enemy ���� �ڷ�ƾ ����");
        defaultDamageCollider.EnableMeleeArea();
        yield return new WaitForSeconds(0.1f);
        defaultDamageCollider.DisableMeleeArea();
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
