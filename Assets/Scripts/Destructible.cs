using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;

    [Range(0f, 1f)]// tạo slice trên inpector
    public float itemSpawnChance = 0.2f; // tỉ lệ
    public GameObject[] spawnableItems; // kéo item vào

    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)//20%
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
