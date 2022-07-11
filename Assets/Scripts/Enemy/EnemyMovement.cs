using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Vector2 direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.up;
    }
    private void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Walls"))
        {
            transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            if (direction == Vector2.up)    direction = Vector2.right;
            else if (direction == Vector2.right) direction = Vector2.down;
            else if (direction == Vector2.down)  direction = Vector2.left;
            else if (direction == Vector2.left)  direction = Vector2.up;
            Debug.Log("va cham wall  ");
        }   
        
    }

}
