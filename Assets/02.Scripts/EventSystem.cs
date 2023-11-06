using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem instance;
    void Awake()
    {
        // GameManager �ν��Ͻ��� ���� ���, �� �ν��Ͻ��� �����ϵ��� ��
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //�� �Űܵ� ���ı�
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
