using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNPC : MonoBehaviour
{
    public GameObject uiGroup;
    public SphereCollider recoRange;
    public Image pressE;

    bool isOpenAble = false;

    public float shopRecoRange = 2.0f;    //OnDrawGizmos() 범위 표시용 float(거리)
    public Vector3 originTransform;

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shopRecoRange);
    }

    private void Start() {
        pressE.enabled = false;
    }

    private void Update() {
        if (isOpenAble) {
            if (Input.GetKeyDown(KeyCode.E)) {
                uiGroup.SetActive(true);
                pressE.enabled = false;
            }
        }
    }



    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            Debug.Log("enter Player");
            isOpenAble = true;
            pressE.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("exit Player");
            isOpenAble = false;
            uiGroup.SetActive(false);
            pressE.enabled = false;
        }
    }
    

    /* if (Input.GetKeyDown("e")) {
                Debug.Log("enter E button");
                if (!isOpen) {
                    isOpen = true;
                    //uiGroup.anchoredPosition = Vector3.zero;
                    uiGroup.SetActive(true);
                }
                else {
    isOpen = false;
    //uiGroup.anchoredPosition = Vector3.down * 1000;
    uiGroup.SetActive(false);
}
*/
}

