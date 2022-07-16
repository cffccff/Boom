using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItem : MonoBehaviour
{
    public ItemPickup[] item;
    public void remove()
    {
        item = FindObjectsOfType<ItemPickup>();
        foreach (ItemPickup item in item)
        {
            Destroy(item.gameObject);
        }
    }
}
