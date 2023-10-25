using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    public static PlayerCanvas instance; // �̱��� �ν��Ͻ�

    private void Awake() {
        if (instance == null) {
            instance = this; // �� �ν��Ͻ��� �̱������� ����
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else {
            Destroy(gameObject); // �̹� �ٸ� �ν��Ͻ��� ������ �� �ν��Ͻ� �ı�
        }
    }
}
