using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbag : MonoBehaviour
{
    Material mat;
    public BoxCollider attackArea;
    public BoxCollider powerAttackArea;
    bool isAttack = false;
    int attackCount = 0;

    private void Awake() {
        mat = GetComponentInChildren<MeshRenderer>().material;
    }

    private void FixedUpdate() {
        Targeting();
    }

    void Targeting() {              // ���� �ν��ϱ� ���� Ÿ���� �Լ�
        float targetRadius = 5f;  // Ž�� ���Ǿ��� ������ ����
        float targetRange = 8f;     // Ž�� ���Ǿ� ĳ��Ʈ�� �ִ� �Ÿ� ����

        // ���Ǿ� ĳ��Ʈ�� ���� �÷��̾ Ž��
        RaycastHit[] rayHits = Physics.SphereCastAll(
            transform.position,                 // ���Ǿ� ĳ��Ʈ�� ���� ��ġ
            targetRadius,                       // ���Ǿ��� ������
            transform.forward,                  // ���Ǿ� ĳ��Ʈ�� ���� (����)
            targetRange,                        // ���Ǿ� ĳ��Ʈ�� �ִ� Ž�� �Ÿ�
            LayerMask.GetMask("Player")         // �÷��̾� ���̾ �ش��ϴ� ��� �˻�
        );

        // ���� ���Ǿ� ĳ��Ʈ�� �÷��̾ ����ǰ�, ���� ���� ���� �ƴ� ���
        if (rayHits.Length > 0 && !isAttack) {
            // ���� ���� �ڷ�ƾ�� SandBag_Attack()�� ����
            StartCoroutine(SandBag_Attack());
        }
    }

    IEnumerator SandBag_Attack() {
        isAttack = true;

        if (attackCount < 2) {             //enemyAttack �Ϲݰ���
            mat.color = Color.green;
            yield return new WaitForSeconds(1f);
            mat.color = Color.yellow;
            yield return new WaitForSeconds(1f);
            mat.color = Color.magenta;
            attackArea.enabled = true;
            yield return new WaitForSeconds(0.1f);
            attackArea.enabled = false;
            yield return new WaitForSeconds(0.5f);
            attackCount++;
        }
        else if (attackCount >= 2) {         //enemyPowerAttack ���Ѱ���
            attackCount = 0;
            mat.color = Color.yellow;
            yield return new WaitForSeconds(0.3f);
            mat.color = Color.magenta;
            yield return new WaitForSeconds(0.3f);
            mat.color = Color.yellow;
            yield return new WaitForSeconds(0.3f);
            mat.color = Color.magenta;
            yield return new WaitForSeconds(0.3f);
            mat.color = Color.cyan;
            powerAttackArea.enabled = true;
            yield return new WaitForSeconds(0.1f);
            powerAttackArea.enabled = false;
            yield return new WaitForSeconds(1f);
        }


        isAttack = false;
    }
}
