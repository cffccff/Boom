using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed=2f;

    [Header("Health")] 
    [SerializeField] private int maxHealth=1;
    [SerializeField] private int currentHealth;
    [SerializeField] private int takeDamege;

    protected Tilemap tilemap;

    private Animator animator;

    private Rigidbody2D rb;
    public Material flashMaterial;
    public Material defaultMaterial;
    private Collider2D coll;
    private SpriteRenderer spriteRenderer;
    public GameObject[] wallChecks = new GameObject[4]; // bottom, top, right, left
    readonly int[,] directions = new int[,] { { 0, -1 }, { 0, 1 }, { 1, 0 }, { -1, 0 } }; // bottom, top, right, left

   [SerializeField] protected bool directionChosen = false;

   private int index;
    public GameManager gameManager;

   private Vector2 movement;
    private float count;

    [Header("Gold")]
    [SerializeField] int gold = 100;
    private UiGold uiGold;
    void Start()
    {
        uiGold = FindObjectOfType<UiGold>();

        SetUpReference();
        SetUpStat();
    }
    private void SetUpReference()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GameObject.Find("Sky").GetComponent<Tilemap>();
    }
    protected virtual void SetUpStat()
    {
      //  moveSpeed = 2f;
       // maxHealth = 1;
        currentHealth = maxHealth;
        takeDamege = maxHealth - 1;


    }
    protected virtual void Update()
    { 
        ///Set enemy direction if not chosen
        ///Check obstacles in enemy direction
        RandomDirection();
        SetAnimation();
    }
    protected virtual void RandomDirection()
    {
        if (!directionChosen) //enemy can't always choose every possible way
        {
            
            GetDirection();
        }
        if (ObstacleThere(wallChecks[index]))
        {
           
            movement.x = 0;
            movement.y = 0;
            directionChosen = false;
        }
    }
    private void SetAnimation()
    {
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        if (movement.x >= 1) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    void FixedUpdate()
    {
        ///Enemy Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

   private void GetDirection()
    {
        ///Set new random direction, not to an obstacle though 
        System.Random rnd = new System.Random();
        index = rnd.Next(4);
        while (ObstacleThere(wallChecks[index]))
        {
            index = rnd.Next(4);
        }
        movement.x = directions[index, 0];
        movement.y = directions[index, 1];
        movement.Normalize();
        directionChosen = true;
    }

    protected virtual bool ObstacleThere(GameObject check)
    {
        ///Decide, if obstacle is at a position of the enemy wallCheck object
        Vector3 worldPos = check.transform.position;
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        if (tilemap.GetTile(cell) != null) //tile block
        {
            return true;
        }
        try
        {
            Vector3 bombPos = GameObject.FindGameObjectWithTag("Bomb").transform.position;
            Vector3Int bombCell = tilemap.WorldToCell(bombPos);
            if (cell == bombCell) //bomb block - only works for one bomb at a time
            {
                return true;
            }
            
        }
        catch (System.NullReferenceException) //no bomb placed
        {
        }
        return false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            if (currentHealth <= 0) return;
            currentHealth = takeDamege;
            StartCoroutine(Hurt());
            if (currentHealth <= 0)
            {
                moveSpeed = 0f;
                coll.isTrigger = true;
                Invoke(nameof(DestroyObject), 1.2f);
                return;
            }

          
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Enemy collider");
            movement = -movement;
            directionChosen = false;
        }

    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<PlayerHealth>().TakeDamage();
        }
    }

    private IEnumerator Hurt()
    {
        Physics2D.IgnoreLayerCollision(8, 13, true);

        for (float i = 0; i <= 0.5; i += 0.1f)
        {
            spriteRenderer.material = flashMaterial;

            yield return new WaitForSeconds(.05f);

            spriteRenderer.material = defaultMaterial;
            yield return new WaitForSeconds(.05f);
        }

        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(8, 13, false);
        takeDamege = currentHealth - 1;
    }
    private void DestroyObject()
    {
        gameManager.CheckWinStage();
        gameObject.SetActive(false);
        uiGold.addGold(gold);

    }

}
