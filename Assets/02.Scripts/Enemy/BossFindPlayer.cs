using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFindPlayer : MonoBehaviour
{
    private Transform player;
    private float distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer < 20)
        {
            GameManager.instance.isBossBattle = true;
        }
    }
}
