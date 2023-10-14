using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLerf : MonoBehaviour
{

    public float smoothTime = 2f;
    Transform original;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {    //우클릭 키 다운 시 패링
            OnParried();
        }
    }

    IEnumerator SmoothPushed(Vector3 current, Vector3 target, float time) {      //캐릭터 z값만큼 뒤로 밀려남
        Vector3 velocity = Vector3.zero;
        Debug.Log("--------스무스 온");
        this.transform.position = current;
        float offset = 0.01f;
        while (target.z + offset <= this.transform.position.z) {
            this.transform.position
                = Vector3.SmoothDamp(this.transform.position, target, ref velocity, time);
            yield return null;
        }

        yield return null;
    } 
        private void OnParried() {
            Debug.Log("패리 온------");
            original = this.transform;
        StartCoroutine(SmoothPushed(original.position,
                   original.position - new Vector3(0, 0, 5),
                    smoothTime));
        //StartCoroutine(LerpPushed(original.position, original.position + new Vector3(0, 0, 4)));
        //parryParticle.Play();
    }

    /*
        IEnumerator SmoothPushed(Vector3 current, Vector3 target, float time) {      //캐릭터 z값만큼 뒤로 밀려남
            Vector3 velocity = Vector3.zero;
            Debug.Log("--------스무스 온");
            //this.transform.position = current;
            float offset = 0.01f;      //무한히 반복되는걸 막기위한 작은 수
            while (this.transform.position.z <= target.z + offset) {
                this.transform.position
                    = Vector3.SmoothDamp(current, target, ref velocity, time);
                yield return null;
            }

            //transform.position = target;

            yield return null;
        }*/
}
