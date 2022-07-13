using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeart : MonoBehaviour
{

    [SerializeField] int startingHealth = 3;
    [SerializeField] int currentHealth;

   // public GameManager gameManager;
   // public EnemyMovement movement;


    private SpriteRenderer spriteRend;
    public Material flashMaterial;
    public Material defaultMaterial;
    private AutoMove autoMoveScript;
    private bool test = false;
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        autoMoveScript = GetComponent<AutoMove>();
        currentHealth = startingHealth;
    }
  
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {

            StartCoroutine(Hurt());

            currentHealth--;
            if (currentHealth <= 0)
            {
                autoMoveScript.enabled = false;
                Invoke(nameof(DestroyObject), 1);
            }
        }
    }
    IEnumerator Hurt()
    {
      
      

        for (float i = 0; i <= 0.5; i+=0.1f)
        {
            spriteRend.material = flashMaterial;

            yield return new WaitForSeconds(.05f);

            spriteRend.material = defaultMaterial;
            yield return new WaitForSeconds(.05f);
        }
      



    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
   

}
