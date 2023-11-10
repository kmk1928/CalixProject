using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int portalNumber;
    public Transform playerSpawn;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            if (playerSpawn == null)
            {
                player.transform.position = new Vector3(0, 0, 0); // 플레이어의 위치를 (0, 0, 0)으로 설정
            }
            else
            {
                player.transform.position = playerSpawn.position;
            }
        }
    }
}
