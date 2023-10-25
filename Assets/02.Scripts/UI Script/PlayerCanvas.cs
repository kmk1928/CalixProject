using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    public static PlayerCanvas instance; // 싱글톤 인스턴스

    private void Awake() {
        if (instance == null) {
            instance = this; // 이 인스턴스를 싱글톤으로 설정
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else {
            Destroy(gameObject); // 이미 다른 인스턴스가 있으면 이 인스턴스 파괴
        }
    }
}
