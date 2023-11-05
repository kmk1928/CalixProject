using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAreaSetup : MonoBehaviour
{
    PlayerController playerCtrl;
    DamageCollider damageCollider;      //���� ������ damageCollider�� �޾ƿ��� ���� ����
    public DamageCollider defaultDamageCollider;
    public DamageCollider oneSlash;
    public DamageCollider flymech;
    public DamageCollider bloodRain;

    private void Start() {
        //���� ���� �ݶ��̴� �ޱ� ���� �÷��̾���Ʈ�ѷ� 
        //�÷��̾� ��ġ Ȯ��
        playerCtrl = GetComponent<PlayerController>();
        LoadDefaultDamageCollider();
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
        yield return new WaitForSeconds(0.08f);
        defaultDamageCollider.DisableMeleeArea();
    }

    void OneSlashAttack1() {
        StartCoroutine(OneSlashAttack1_C());
    }

    IEnumerator OneSlashAttack1_C() {
        oneSlash.EnableMeleeArea();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("������ ����");
        oneSlash.DisableMeleeArea();
    }

    void FlyMechAttack1_2()
    {
        StartCoroutine(FlyMechAttack1_2_C());
    }

    IEnumerator FlyMechAttack1_2_C()
    {
        flymech.EnableMeleeArea();
        yield return new WaitForSeconds(0.1f);
        Debug.Log("���� ������ ����");
        flymech.DisableMeleeArea();
    }

    void BloodRainAttackArea()
    {
        StartCoroutine(BloodRainAttackArea_C());
    }

    IEnumerator BloodRainAttackArea_C()
    {
        bloodRain.EnableMeleeArea();
        yield return new WaitForSeconds(0.05f);
        Debug.Log("������ ����");
        bloodRain.DisableMeleeArea();
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
