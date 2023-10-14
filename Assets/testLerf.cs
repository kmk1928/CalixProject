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
        if (Input.GetMouseButtonDown(1)) {    //��Ŭ�� Ű �ٿ� �� �и�
            OnParried();
        }
    }

    IEnumerator SmoothPushed(Vector3 current, Vector3 target, float time) {      //ĳ���� z����ŭ �ڷ� �з���
        Vector3 velocity = Vector3.zero;
        Debug.Log("--------������ ��");
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
            Debug.Log("�и� ��------");
            original = this.transform;
        StartCoroutine(SmoothPushed(original.position,
                   original.position - new Vector3(0, 0, 5),
                    smoothTime));
        //StartCoroutine(LerpPushed(original.position, original.position + new Vector3(0, 0, 4)));
        //parryParticle.Play();
    }

    /*
        IEnumerator SmoothPushed(Vector3 current, Vector3 target, float time) {      //ĳ���� z����ŭ �ڷ� �з���
            Vector3 velocity = Vector3.zero;
            Debug.Log("--------������ ��");
            //this.transform.position = current;
            float offset = 0.01f;      //������ �ݺ��Ǵ°� �������� ���� ��
            while (this.transform.position.z <= target.z + offset) {
                this.transform.position
                    = Vector3.SmoothDamp(current, target, ref velocity, time);
                yield return null;
            }

            //transform.position = target;

            yield return null;
        }*/
}
