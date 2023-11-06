using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem instance;
    void Awake()
    {
        // GameManager 인스턴스가 없는 경우, 이 인스턴스를 유지하도록 함
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //씬 옮겨도 노파괴
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
