using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pakupa : BaseEnemy
{
    float count;
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
         base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Debug.Log("Item collider");
            Destroy(collision.gameObject);
        }
    }
    protected override void Update()
    {
        base.Update();
        count += Time.fixedDeltaTime;

        if (count >= 60)
        {
            Debug.Log("count " + count);
            Debug.Log("change");
            directionChosen = false;
            count = 0;
        }
    }
}
