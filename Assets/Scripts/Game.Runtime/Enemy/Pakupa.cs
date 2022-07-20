using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pakupa : BaseEnemy
{
    float time_change;
    protected override bool ObstacleThere(GameObject check)
    {
        ///Decide, if obstacle is at a position of the enemy wallCheck object
        Vector3 worldPos = check.transform.position;
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        if (tilemap.GetTile(cell) != null) //tile block
        {
            return true;
        }

        return false;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
         base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("Item collider");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            Debug.Log("Bomb collider");
            Destroy(collision.gameObject);
        }
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            Debug.Log("Bomb collider");
            Destroy(collision.gameObject);
        }
    }
    protected override void RandomDirection()
    {
        base.RandomDirection();
        time_change += Time.fixedDeltaTime;

        if (time_change >= 60)
        {
            //Debug.Log("time_change " + time_change);
            //Debug.Log("change");
            directionChosen = false;
            time_change = 0;
        }
    }
}
