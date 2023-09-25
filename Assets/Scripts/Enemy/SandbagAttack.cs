using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandbagAttack : MonoBehaviour
{
    Material mat;
    public BoxCollider attackArea;
    bool isAttack = false;

    private void Awake()
    {
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
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.5f);
        attackArea.enabled = true;
        yield return new WaitForSeconds(0.3f);
        attackArea.enabled = false;

        isAttack = false; 
    }

}
