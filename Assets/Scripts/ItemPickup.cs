using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
        Heart,
    }

    public ItemType type;
    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;

            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius++;
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<Movement>().speed++;
                break;
            case ItemType.Heart:
                player.GetComponent<Health>().currentHealth++;
                break;

        }

        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)// phát hiện player
    {
        if (collision.CompareTag("Player"))
        {
            OnItemPickup(collision.gameObject);
        }
    }
}
