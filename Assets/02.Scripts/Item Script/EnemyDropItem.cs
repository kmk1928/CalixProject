using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject[] itemPrefabs;

    private bool hasDroppedItem = false; 

    private void OnDestroy()
    {
        if (!hasDroppedItem)
        {
            DropItem();
            hasDroppedItem = true;
        }
    }

    private void DropItem()
    {
        if (itemPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject itemPrefab = itemPrefabs[randomIndex];

            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }
}