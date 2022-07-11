using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
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
        }
    }
    private void takeDamage()
    {
        currentHealth--;
        StartCoroutine(Invunerability());

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
        
    }
    private IEnumerator Invunerability()// bất tử
    {
        Physics2D.IgnoreLayerCollision(8, 9, true); // bỏ qua va chạm layer (player and explotion)

        yield return new WaitForSeconds(iFramesDuration);

        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
