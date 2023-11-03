using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance;
    private void Awake()
    {
        instance = this;
    }

    public List<ItemData> itemDB = new List<ItemData>();

    public GameObject ItemPrefab;
    public Vector3[] pos;

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject it = Instantiate(ItemPrefab, pos[i],Quaternion.identity);
            it.GetComponent<GetItem>().SetItem(itemDB[Random.Range(0, 3)]);

        }
    }

}
