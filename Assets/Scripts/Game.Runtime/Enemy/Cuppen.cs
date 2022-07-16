using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuppen : BaseEnemy
{
    protected override bool ObstacleThere(GameObject check)
    {
        ///Decide, if obstacle is at a position of the enemy wallCheck object
        Vector3 worldPos = check.transform.position;
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        if (tilemap.GetTile(cell) != null) //tile block
        {
            if (tilemap.GetTile(cell).name.StartsWith("1_5"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        try
        {
            Vector3 bombPos = GameObject.FindGameObjectWithTag("Bomb").transform.position;
            Vector3Int bombCell = tilemap.WorldToCell(bombPos);
            if (cell == bombCell) //bomb block - only works for one bomb at a time
            {
                return true;
            }
            
        }
        catch (System.NullReferenceException) //no bomb placed
        {
        }
        return false;
    }
}
