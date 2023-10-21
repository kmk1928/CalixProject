using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanPlayer : MonoBehaviour
{
    public GameObject playerPrefab; // �÷��̾� �������� Inspector���� ����

    void Awake() {
        // �÷��̾� �������� �����Ͽ� ���� ����
        GameObject playerClone = Instantiate(playerPrefab, transform.position, Quaternion.identity);

        // ������ �÷��̾ ���� �� ���� ������Ʈ�� ��ü
        Destroy(gameObject); // �� ���� ������Ʈ ����
    }
}
