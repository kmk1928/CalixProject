using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownMapPlayer : MonoBehaviour
{
    public Transform resqwnTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = resqwnTransform.position;
        }
    }

}
