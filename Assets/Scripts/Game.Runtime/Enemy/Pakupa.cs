using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pakupa : BaseEnemy
{
    float time_change;
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
         base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("Item collider");
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
