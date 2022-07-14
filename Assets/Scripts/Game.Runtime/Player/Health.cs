﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public Canvas gameOver;
    public int startingHealth =3;
    public int currentHealth;

    public AnimatedSprite activeSpriteDeath;
    public Movement movement;

    [Header("iFrames")] 
    [SerializeField] private float iFramesDuration; // thời gian bất tử
    private SpriteRenderer spriteRend;
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        currentHealth = startingHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)// va chạm nổ 
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            takeDamage();
            Debug.Log("bi dinh bom");
        }
    }
    public void takeDamage()
    {
        currentHealth--;
        //StartCoroutine(Invunerability());

        if (currentHealth <= 0)
        {
            movement.death();
            movement.enabled = false;
            activeSpriteDeath.enabled = true;

            Invoke(nameof(Death), 1.25f);
        }       
    }
    private void Death()
    {
        gameObject.SetActive(false);
        gameOver.enabled = true;// bật canvas gameover
    }
    private IEnumerator Invunerability()// bất tử
    {
        Physics2D.IgnoreLayerCollision(8, 9, true); // bỏ qua va chạm layer (player and explotion)
        Physics2D.IgnoreLayerCollision(9, 13, true);

        yield return new WaitForSeconds(iFramesDuration);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(9, 13, false);
    }
}
