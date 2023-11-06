using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoad : MonoBehaviour
{
    public string sceneName; // �̵��� ���� �̸��� Inspector���� ������ ����

    // Ŭ�� �� �̺�Ʈ�� ó���ϴ� �Լ�
    private void OnMouseDown()
    {
        ChangeScene();
    }

    // ���� �����ϴ� �Լ�
    public void ChangeScene()
    {
        // sceneName ������ ����� ������ �̵�
        SceneManager.LoadScene(sceneName);
    }
}

