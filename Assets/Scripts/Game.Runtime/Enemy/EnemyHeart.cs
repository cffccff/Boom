using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeart : MonoBehaviour
{

    public int startingHealth = 3;
    public int currentHealth;

    public GameManager gameManager;
    public EnemyMovement movement;

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            if (currentHealth == 0) return;
            takeDamage();
            //đang có buff đứng giữa 2 explosion thì bị ăn 2 damage
        }
    }
    private void takeDamage()
    {
        currentHealth--;
        StartCoroutine(Invunerability());

        if (currentHealth <= 0)
        {
            movement.enabled = false;           
            Invoke(nameof(Death), 1.25f);
            return;
        }       
    }
    private void Death()
    {
        gameManager.CheckWinStage();
        gameObject.SetActive(false);
    }
    private IEnumerator Invunerability()// bất tử
    {
        Physics2D.IgnoreLayerCollision(8, 9, true); // bỏ qua va chạm layer (player and explotion)

        yield return new WaitForSeconds(iFramesDuration);

        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
