using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float increasedGravity = 3.0f; // 증가시킬 중력 값

    void Start()
    {
        // 플레이어 Rigidbody에 더 높은 중력 적용
        playerRigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= increasedGravity;
    }
}
