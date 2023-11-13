using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float increasedGravity = 3.0f; // ������ų �߷� ��

    void Start()
    {
        // �÷��̾� Rigidbody�� �� ���� �߷� ����
        playerRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= increasedGravity;
    }
}
