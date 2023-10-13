using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSandBag : MonoBehaviour
{
    CharCombat combat;
    Material mat;
    public BoxCollider attackArea;
    public BoxCollider powerAttackArea;
    bool isAttack = false;
    int attackCount = 0;

    private void Awake() {
        combat = GetComponent<CharCombat>();
        mat = GetComponentInChildren<MeshRenderer>().material;
    }

    void Targeting() {                  //���� �ν��ϱ� ���� Ÿ����
        float targetRadius = 1.5f;
        float targetRange = 3f;

        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position,
                                  targetRadius,                     //����
                                  transform.forward,              //������
                                  targetRange,                    //���� �Ÿ�
                                  LayerMask.GetMask("Player"));   //���̾ �÷��̾��� ���

        if (rayHits.Length > 0 && !isAttack) {       //rayhit������ �����Ͱ� ������ ���� �ڷ�ƾ ���� 
            StartCoroutine(SandBag_Attack());
        }
    }

    private void FixedUpdate() {   //Ÿ������ ���� �Ƚ��������Ʈ
        Targeting();
    }

    IEnumerator SandBag_Attack() {
        isAttack = true;

        if (attackCount < 2) {             //enemyAttack �Ϲݰ���
            mat.color = Color.green;
            yield return new WaitForSeconds(0.5f);
            mat.color = Color.yellow;
            yield return new WaitForSeconds(0.5f);
            mat.color = Color.magenta;
            attackArea.enabled = true;
            yield return new WaitForSeconds(0.3f);
            attackArea.enabled = false;
            attackCount++;
        }
        else if(attackCount >= 2) {         //enemyPowerAttack ���Ѱ���
            attackCount = 0;
            mat.color = Color.yellow;
            yield return new WaitForSeconds(0.2f);
            mat.color = Color.magenta;
            yield return new WaitForSeconds(0.2f);
            mat.color = Color.yellow;
            yield return new WaitForSeconds(0.2f);
            mat.color = Color.magenta;
            yield return new WaitForSeconds(0.2f);
            mat.color = Color.cyan;
            powerAttackArea.enabled = true;
            yield return new WaitForSeconds(0.3f);
            powerAttackArea.enabled = false;

        }


        isAttack = false;
    }

}
