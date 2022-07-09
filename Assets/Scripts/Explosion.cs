using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSprite start;// đang tắt
    public AnimatedSprite middle;
    public AnimatedSprite end;

    public void SetActiveRenderer(AnimatedSprite renderer)// bật component truyền vào và tắt component khác
    {
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }
    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);// góc tính = pi
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);//xoay 
    }
    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
