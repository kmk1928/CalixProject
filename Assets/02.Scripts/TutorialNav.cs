using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNav : MonoBehaviour
{
    public GameObject yourUI; // Ư�� UI ��ü�� Inspector���� �Ҵ�
    public GameObject navUI;

    // �÷��̾ Ʈ���� ������ �������� �� ȣ���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾���� �浹 ���� Ȯ��
        {
            yourUI.SetActive(true); // UI�� Ȱ��ȭ
            navUI.SetActive(true);
        }
    }

    // �÷��̾ Ʈ���� �������� ������ �� ȣ���
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾���� �浹 ���� Ȯ��
        {
            yourUI.SetActive(false); // UI�� ��Ȱ��ȭ
            navUI.SetActive(false);
        }
    }
}
