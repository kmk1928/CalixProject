using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour {
    CharCombat combat;
    public Transform target;

    // Start is called before the first frame update
    void Start() {
        combat = GetComponent<CharCombat>();
    }

    // Update is called once per frame
    void Update() {
        MonsterAI();
    }


    void MonsterAI() {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > 1.3f) {
        }
        else if (distance <= 1.3f) {
            CharStats targetStats = target.GetComponent<CharStats>();
            if (targetStats != null) {
                combat.Attack(targetStats);//Do melee attack when near target
            }
        }
    }
}