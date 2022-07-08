using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask whatStopsMovement;
    private void Start()
    {
        movePoint.parent = null;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)// nếu khoảng cách nhỏ hơn 0.05 khi bấm di chuyển
        {
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) //nếu bấm nút đạt tới -1,1 
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"),0f,0f), .2f, whatStopsMovement))// tâm cách player 1 đơn vị và bán kính là 0,2 nên giúp chỉ phát hiện duy nhất 1 vật cản.
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);// nếu không phát hiện vật cản thì point dịch chuyển tới vị trí mới từ đó player di chuyển dần tới point
                }
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f,Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }
    }
}

