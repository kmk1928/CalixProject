using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttackCollider : MonoBehaviour
{
    public float damage = 10;
    public float activationInterval = 0.4f; // �� Collider�� Ȱ��ȭ ���� (�� ����)

    private BoxCollider[] colliders; // "ab" ���� ������Ʈ�� ��� Collider �迭
    private int currentColliderIndex = 0; // ���� Ȱ��ȭ�� Collider�� �ε���

    private void Start()
    {
        colliders = GetComponents<BoxCollider>();

        if (colliders.Length >= 2)
        {
            // ���� �� ù ��° Collider Ȱ��ȭ
            StartCoroutine(ActivateCollider());
        }
        else
        {
            Debug.LogError("���� ������Ʈ 'ab'�� ��� 2���� Box Collider�� �ʿ��մϴ�.");
        }
    }

    private IEnumerator ActivateCollider()
    {
        while (currentColliderIndex < colliders.Length)
        {
            colliders[currentColliderIndex].enabled = true; // ���� Collider�� Ȱ��ȭ

            yield return new WaitForSeconds(activationInterval);

            colliders[currentColliderIndex].enabled = false; // ���� Collider�� ��Ȱ��ȭ

            // ���� Collider�� �̵�
            //currentColliderIndex = (currentColliderIndex + 1) % colliders.Length;
            currentColliderIndex += 1;
        }
        Debug.Log("�۵��Ȱ��� Ȯ��");
    }
}
