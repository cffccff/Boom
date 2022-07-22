using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    public SaveGold saveGold;
    private Rigidbody2D rb;
    private Collider2D coll;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    public float initialSpeed = 5f;
    public float moveSpeed;
    void Start()
    {
        saveGold = FindObjectOfType<SaveGold>();
        SetUpReference();
        resetBuff();

    }
    public void resetBuff()// dùng khi vừa vào scene và khi load map() chưa update
    {
        moveSpeed = initialSpeed + saveGold.speedLevel * 0.5f;
    }
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
      

        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            movement.y = 0;
        }
        else
        {
            movement.x = 0;
        }
        SetAnimation();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void SetUpReference()
    {
      
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void SetAnimation()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
       
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
}
