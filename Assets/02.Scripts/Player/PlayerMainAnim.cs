using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainAnim : MonoBehaviour
{
    Animator anim; // 애니메이션 변수

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>(); // 애니메이션
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("standTrigger");
            Debug.Log("일어난다");
        }
    }
}
