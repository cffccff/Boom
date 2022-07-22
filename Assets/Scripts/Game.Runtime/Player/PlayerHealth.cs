using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject gameOver;

    [Header("Health")]
    public int startingHealth = 3;
    public int currentHealth;
    public int _takeDamage;
    [SerializeField] Image healthBar;
    public Material flashMaterial;
    public Material defaultMaterial;
    private SpriteRenderer spriteRenderer;
    [Header("iFrames")]
    [SerializeField] private float iFramesDuration = 0.6f;

    private Collider2D collider;
    [SerializeField] GameObject gameManager;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        healthBar.fillAmount = startingHealth * 0.1f;
        _takeDamage = startingHealth - 1;
        currentHealth = startingHealth;
        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(9, 13, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            TakeDamage();
            Debug.Log("player bombed");
        }
    }
    public void AddHeart()
    {
        if (currentHealth == 10) return;

        currentHealth++;
        _takeDamage++;
        healthBar.fillAmount += 0.1f;
    }
    public void TakeDamage()
    {
        currentHealth = _takeDamage;
        healthBar.fillAmount = currentHealth * 0.1f;
        if (currentHealth > 0)
        {
            transform.position = new Vector3(1, 0, 0);
           
        }

        if (currentHealth <= 0)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            animator.SetTrigger("isDeath");
            Invoke(nameof(Death), 1.25f);
            return;
        }
        StartCoroutine(Invunerability());
    }
    private void Death()
    {
        gameObject.SetActive(false);
        gameOver.SetActive(true);
        //Time.timeScale = 0;
        gameManager.SetActive(false);
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(9, 13, true);
        //collider.enabled = false;


        for (float i = 0; i <= 0.5; i += 0.1f)
        {

            spriteRenderer.material = flashMaterial;

            yield return new WaitForSeconds(.05f);

            spriteRenderer.material = defaultMaterial;
            yield return new WaitForSeconds(.05f);
            
        }
        yield return new WaitForSeconds(iFramesDuration);

        Physics2D.IgnoreLayerCollision(8, 9, false);
        Physics2D.IgnoreLayerCollision(9, 13, false);
        //collider.enabled = true;

        _takeDamage = currentHealth - 1;
    }
}
