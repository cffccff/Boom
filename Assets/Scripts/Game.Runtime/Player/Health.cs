using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public AnimatedSprite activeSpriteDeath;
    public GameObject gameOver; // truyeefn screen Lose vao
    public Movement movement;

    [Header("Health")]
    [SerializeField] Image healthBar;
    public int startingHealth =3;
    public int currentHealth;
    public int _takeDamage;   

    [Header("iFrames")] 
    [SerializeField] private float iFramesDuration =0.5f; // thời gian bất tử
    private SpriteRenderer spriteRend;
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        healthBar.fillAmount = startingHealth * 0.1f;
        _takeDamage = startingHealth - 1;
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
    public void addHeart()
    {
        if (currentHealth == 10) return;

        currentHealth++;
        _takeDamage++;
        healthBar.fillAmount += 0.1f;
    }
    public void takeDamage()
    {
        currentHealth = _takeDamage;
        healthBar.fillAmount = currentHealth * 0.1f;
        if (currentHealth > 0) transform.position = new Vector3(1, 0, 0);

        if (currentHealth <= 0)
        {
            movement.death();
            movement.enabled = false;
            activeSpriteDeath.enabled = true;

            Invoke(nameof(Death), 1.25f);
            return;
        }
        StartCoroutine(Invunerability());      
    }
    private void Death()
    {
        gameObject.SetActive(false);
        gameOver.SetActive(true);// bật canvas gameover
        Time.timeScale = 0;// pause
    }
    private IEnumerator Invunerability()// bất tử
    {
        Physics2D.IgnoreLayerCollision(8, 9, true); // bỏ qua va chạm layer (player and explotion)
        Physics2D.IgnoreLayerCollision(9, 13, true);

        yield return new WaitForSeconds(iFramesDuration);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(9, 13, false);

        _takeDamage = currentHealth -1;
    }
}
